using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour {

	//private float defaultSlideDashPower;
	public UnityEvent OnPlayerRevive;

	private float defaultDashPower;
	public string abilityType = "Speed";
	public string shootDirection = "Right";
	public Vector2 shootTarget;
	public bool onGround = false;
	public bool isFlight = false;
	public bool isSlideDashing = false;
	public bool jumpingAwayFromWall = false;
	public bool inMidAir = false;
	public bool cantMove = false;
	public bool isAlive = true;
	public Vector3 lastCheckpoint;


	[HideInInspector]
	public List<Transform> shootableTransforms = new List<Transform>();
	[HideInInspector]
	public List<Transform> resetObjectsOnDeath = new List<Transform>();
	
	private Rigidbody myRigidbody;
	private Animator myAnimator;
	private BoxCollider myBoxCollider;
	private SpriteRenderer spriteRenderer;
	private Transform myTransform;
	private Transform gunStartTransform;

	private string rememberShootDirection;

	private void Start() {
		myTransform = GetComponent<Transform>();
		
		//defaultSlideDashPower = GetComponent<PlayerAbilitiesController>().slideDashPower;
		defaultDashPower = GetComponent<PlayerDashController3D>().flightPower;

		StartCoroutine(FallUnderWorldUpdate());
		lastCheckpoint = transform.position;
		myAnimator = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		myBoxCollider = GetComponent<BoxCollider>();
		gunStartTransform = transform.Find("GunStart");
	}
	private void Update() {

		CheckOnGround(); // I cant just put this in OnCollisionEnter because it doesn't work well with slides

		if (hardInput.GetKeyDown("Restart")) {
			Revive();
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			GameManager.gameManagerGameObject.GetComponent<GameManager>().TogglePauseGame();
			cantMove = GameManager.pausedGame;
		}
		// play animation //

		myAnimator.SetBool("IsSlideDashing", isSlideDashing);
		myAnimator.SetBool("IsAlive", isAlive);
		myAnimator.SetBool("OnGround", onGround);
		myAnimator.SetBool("IsFlight", isFlight);
		// Direction
		if (rememberShootDirection != shootDirection) {

			rememberShootDirection = shootDirection;

			if (shootDirection == "Right") {
				myAnimator.SetBool("Direction", true);
				spriteRenderer.flipX = false;
				gunStartTransform.localPosition = new Vector3(1, gunStartTransform.localPosition.y, 0f);
			}
			else {
				myAnimator.SetBool("Direction", false);
				spriteRenderer.flipX = true;
				gunStartTransform.localPosition = new Vector3(-1, gunStartTransform.localPosition.y, 0f);
			}
		}
		// IsMoving
		if (Mathf.Abs(myRigidbody.velocity.x) > 1f) {
			myAnimator.SetBool("IsMoving", true);
		}
		else {
			myAnimator.SetBool("IsMoving", false);
		}
	}
	private void FixedUpdate() {

		SetTarget();


		// getting inMidAir
		if (onGround == false) {
			inMidAir = true;
		}
		else {
			inMidAir = false;
		}
	}
	private bool LeftOfPlayer(Vector2 targetPosition) {
		if (targetPosition.x < myTransform.position.x + 0.5f) {
			return true;
		}
		return false;
	}
	private bool RightOfPlayer(Vector2 targetPosition) {
		if (targetPosition.x > myTransform.position.x - 0.5f) {
			return true;
		}
		return false;
	}
	private void SetTarget() {
		List<Transform> possibleTargets = new List<Transform>();

		foreach (Transform target in shootableTransforms) {
			if ((shootDirection == "Right" && RightOfPlayer(target.position) == true)
				|| (shootDirection == "Left" && LeftOfPlayer(target.position) == true)) {

				if (Vector2.Distance(target.position, gunStartTransform.position) < 18f) {
					//RaycastHit2D hit = Physics2D.Raycast(gunStartTransform.position, target.GetComponent<Collider2D>().bounds.center - gunStartTransform.position, 15f);
					RaycastHit hit;
					Physics.Linecast(gunStartTransform.position, target.GetComponent<Collider>().bounds.center, out hit);
					
					if (hit.collider != null && hit.collider.tag == "Targetable") {

						possibleTargets.Add(target);
					}
				}
			}
		}

		// sort targets by closest
		possibleTargets.Sort(delegate (Transform a, Transform b) {
			return Vector2.Distance(myTransform.position, a.position)
			   .CompareTo(Vector2.Distance(myTransform.position, b.position));
		});

		if (possibleTargets.Count > 0) {
			shootTarget = possibleTargets[0].GetComponent<Collider>().bounds.center;

		} else {
			// straight ahead
			shootTarget = ReturnAheadPoint();
		}
	}

	private Vector2 ReturnAheadPoint() {
		if (shootDirection == "Right") {
			return gunStartTransform.position + new Vector3(7, 0);
		} else {

			return gunStartTransform.position + new Vector3(-7, 0);
		}
	}
	private void ReturnToLastCheckpoint() {
		myTransform.position = lastCheckpoint;
	}
	public void CameraQuickKickUp(float rate = 0.1f) {
		Camera.main.GetComponent<CameraFOVLerp>().FOVSetTo(9f);
		Camera.main.GetComponent<CameraFOVLerp>().FOVLerpTo(Camera.main.GetComponent<CameraFOVLerp>().defaultFOVSize, rate);
	}
	public void CameraQuickKickDown(float rate = 0.1f) {
		Camera.main.GetComponent<CameraFOVLerp>().FOVSetTo(7f);
		Camera.main.GetComponent<CameraFOVLerp>().FOVLerpTo(Camera.main.GetComponent<CameraFOVLerp>().defaultFOVSize, rate);
	}

	public void Revive() {

		if (OnPlayerRevive != null) {

			OnPlayerRevive.Invoke();
		}

		GetComponent<Health>().hp = 100;
		GetComponent<PlayerWeaponController>().ResetBulletsInChamber();
		GetComponent<PlayerDashController3D>().flightPower = defaultDashPower;
		GetComponent<PlayerDashController3D>().FlightEnd();
		GetComponent<PlayerDashController3D>().SlideDashEnd();
		myRigidbody.velocity = new Vector2(0, 0);
		StopCoroutine("ReviveCountdown");

		//UpdateHUDHealth();
		Camera.main.GetComponent<CameraFOVLerp>().FOVLerpTo(Camera.main.GetComponent<CameraFOVLerp>().defaultFOVSize, 0.1f);
		GameObject.Find("Trail").GetComponent<TrailRenderer>().enabled = true;

		for(int i = 0; i < resetObjectsOnDeath.Count; i += 1) {
			resetObjectsOnDeath[i].GetComponent<ResetObjectOnPlayerDeath>().ResetState();
		}

		isAlive = true;
		cantMove = false;
		ReturnToLastCheckpoint();
	}
	public void Die() {
		GameObject.Find("Trail").GetComponent<TrailRenderer>().enabled = false;
		Camera.main.GetComponent<CameraFOVLerp>().FOVLerpTo(6, 0.05f);
		// GameManager.gameManagerObject.GetComponent<TimeScaleControl>().PlayerDeathSlowDown();

		StartCoroutine("ReviveCountdown");

		isAlive = false;
		cantMove = true;
		GetComponent<PlayerDashController3D>().SlideDashEnd();
		GetComponent<PlayerDashController3D>().FlightEnd();
	}
	IEnumerator ReviveCountdown() {
		yield return new WaitForSeconds(1.8f);

		Revive();
	}
	IEnumerator FallUnderWorldUpdate() {
		while(true) {

			yield return new WaitForSeconds(1f);

			if (myTransform.position.y <= -100) {
				Die();
			}
		}
	}

	// onGround set to false in Jump
	private void CheckOnGround() {

		//float minusExtentsY = -myBoxCollider.bounds.extents.y;
		float minusExtentsY = myBoxCollider.center.y;

		RaycastHit hit1;
		RaycastHit hit2;

		Physics.Raycast(myTransform.position + new Vector3(0.5f, minusExtentsY), Vector2.down, out hit1, 1f, (1 << GameManager.worldLayerMask));
		Physics.Raycast(myTransform.position + new Vector3(-0.5f, minusExtentsY), Vector2.down, out hit2, 1f, (1 << GameManager.worldLayerMask));
		
		if (hit1.collider != null) {
			onGround = true;
		} else if (hit2.collider != null) {
			onGround = true;
		}
		else {
			onGround = false;
		}
	}
	/*
	private void OnCollisionExit(Collision collision) {

		CheckOnGround();
	}
	private void OnCollisionEnter(Collision collision) {
		CheckOnGround();
	}
	*/

}
