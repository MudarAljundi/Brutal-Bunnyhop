using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDashController3D : MonoBehaviour {

	public AudioClip startDashClip;
	public Gradient blue_grad;
	public Gradient red_grad;
	public Gradient yellow_grad;
	
	public float flightPower = 100;
	public float dashPower = 100;

	private AudioSource playerAudioSource;
	private AudioSource flightAudioSource;
	private AudioSource flightRechargeAudioSource;
	private ParticleSystem flightParticleSystem;
	private BoxCollider dashCollider;
	private Rigidbody myRigidbody;
	private PlayerStats myPlayerStats;
	private TrailRenderer myTrailRenderer;

	private string slideStartDirection;

	private void Start() {
		myRigidbody = GetComponent<Rigidbody>();
		myPlayerStats = GetComponent<PlayerStats>();
		dashCollider = transform.Find("DashAndFlight").GetComponent<BoxCollider>();
		myTrailRenderer = transform.Find("Trail").GetComponent<TrailRenderer>();
		flightParticleSystem = transform.Find("FlightParticles").GetComponent<ParticleSystem>();

		playerAudioSource = GetComponent<AudioSource>();
		flightAudioSource = transform.Find("DashAndFlight").GetComponent<AudioSource>();
		flightRechargeAudioSource = transform.Find("FlightRechargeAudio").GetComponent<AudioSource>();

		myPlayerStats.OnPlayerRevive.AddListener(StopFlightAudioOnRevive);
	}

	private void StopFlightAudioOnRevive() {

		flightAudioSource.Stop();
		flightRechargeAudioSource.Stop();
	}
	private void Update() {

		if (myPlayerStats.cantMove == true) {
			return;
		}
		
		if (myPlayerStats.isFlight == true) {
			flightPower -= 39.5f * Time.deltaTime;

			if (flightAudioSource.isPlaying == false) {
				flightAudioSource.Play();
			}
		}
		else if (myPlayerStats.isFlight == false) {
			if (myPlayerStats.onGround == true && flightPower < 100) {
				flightPower += 200f * Time.deltaTime;

				if (flightRechargeAudioSource.isPlaying == false) {
					flightRechargeAudioSource.Play();
				}
			} else if (flightRechargeAudioSource.isPlaying == true) {

				flightRechargeAudioSource.Stop();
			}

			if (flightAudioSource.isPlaying == true) {
				flightAudioSource.Stop();
			}
		}
		if (myPlayerStats.isSlideDashing == true) {
			dashPower -= 150f * Time.deltaTime;
		}

		flightPower = Mathf.Clamp(flightPower, 0, 100);
		dashPower = Mathf.Clamp(dashPower, 0, 100);

		if (myPlayerStats.isSlideDashing == false) {
			if ((hardInput.GetKeyDown("Dash") == true || hardInput.GetKeyDown("Down") == true)
			&& myPlayerStats.onGround == true) {

				SlideDashStart();
			}
		}
		
		else if (myPlayerStats.isSlideDashing == true) {

			if (dashPower == 0
			|| hardInput.GetKeyUp("Dash") == true || hardInput.GetKeyUp("Down") == true) {

				SlideDashEnd();
			}

		}
			

		if (hardInput.GetKeyDown("Flight") == true) {
			if (flightPower > 0) {
				FlightStart();
			}
		}
		// UP
		if ((hardInput.GetKey("Flight") == true && flightPower <= 0) || (hardInput.GetKey("Flight") == false && myPlayerStats.isFlight == true)) {

			FlightEnd();
		}
	}


	private void FixedUpdate() {
		
		if (myPlayerStats.cantMove == true) {
			return;
		}

		if (myPlayerStats.isSlideDashing == true) {

			// momentum when slidedashing
			if (myPlayerStats.shootDirection == "Left") {
				myRigidbody.AddForce(new Vector2(-1, 0) * 200, ForceMode.Force);
			}
			else if (myPlayerStats.shootDirection == "Right") {
				myRigidbody.AddForce(new Vector2(1, 0) * 200, ForceMode.Force);
			}

			if (slideStartDirection != myPlayerStats.shootDirection) {
				SlideDashEnd();
			}
		}
	}
	/*
	private void ShotgunsShoot() {
	
		if (powerCooldown == false) {s
			return;
		}
		ChangeAbility("Shotguns");
		GetComponent<BulletSpawner>().MachineGunShoot(transform.position, myPlayerStats.shootTarget);
		StartCoroutine(ShotgunsCountdown());
		canShootShotguns = false;
	}
	*/
	// Case 1 (ground jumping) and case 2 (wall jumping) handled in Movement Controller
	// case 3: inMidAir dash
	//if (myPlayerStats.onWall == "None" && canDash > 0) {

	private void FlightStart() {

		if (myPlayerStats.isSlideDashing == true) {
			return;
		}

		myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);
		if (myPlayerStats.onGround == true) {
			//GetComponent<MovementController>().JumpUp();
			myRigidbody.AddForce(new Vector2(0, 30), ForceMode.Impulse);
		}
		myRigidbody.AddForce(new Vector2(0, 3), ForceMode.Impulse);

		dashCollider.enabled = true;
		myRigidbody.useGravity = false;
		myPlayerStats.isFlight = true;
		myPlayerStats.onGround = false;
		GetComponent<MovementController3D>().canJump = false;

		flightPower -= 10;

		ChangeAbility("Flight");
		flightParticleSystem.Play();
	}
	public void FlightEnd() {
		if (myPlayerStats.isFlight == false) {
			return;
		}
		myPlayerStats.isFlight = false;

		GetComponent<MovementController3D>().canJump = false;

		dashCollider.enabled = false;
		myRigidbody.useGravity = true;
		flightParticleSystem.Stop();
	}
	private void SlideDashStart() {

		if (myPlayerStats.isFlight == true) {
			return;
		}

		slideStartDirection = myPlayerStats.shootDirection;
		myPlayerStats.isSlideDashing = true;

		dashCollider.enabled = true;

		//myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);
		myRigidbody.AddForce(new Vector2(0, -3), ForceMode.Impulse);
		ChangeAbility("Dash");
	}
	public void SlideDashEnd() {

		if (myPlayerStats.isSlideDashing == false) {
			return;
		}

		dashPower = 100f;

		myPlayerStats.isSlideDashing = false;
		dashCollider.enabled = false;

	}
	public void ChangeAbility(string newAbility) {
		myPlayerStats.abilityType = newAbility;

		if (newAbility == "Dash") {
			playerAudioSource.PlayOneShot(startDashClip);
			myTrailRenderer.GetComponent<TrailRenderer>().colorGradient = blue_grad;
		}
		if (newAbility == "Flight") {
			myTrailRenderer.GetComponent<TrailRenderer>().colorGradient = yellow_grad;
		}
		if (newAbility == "Power") {
			myTrailRenderer.GetComponent<TrailRenderer>().colorGradient = red_grad;
		}
	}
}
