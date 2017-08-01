using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HardShellStudios.InputManagerV2;
using UnityEngine.Events;

public class MovementController : MonoBehaviour {

	public AudioClip outOfAmmo;
	public AudioClip jump;
	public AudioClip deathSND;
	public float horizontalMoveForce;
	public float jumpImpulse;

	[HideInInspector]
	public bool canMove = true;
	[HideInInspector]
	public UnityEvent onJump;
	public UnityEvent onRevive;

	public Vector3 checkpointPosition;
	private bool onGround = false;

	private Rigidbody myRigidbody;
	private BoxCollider myBoxCollider;

	// Use this for initialization
	private void Start () {
		myRigidbody = GetComponent<Rigidbody>();
		myBoxCollider = GetComponent<BoxCollider>();
		checkpointPosition = transform.position;

		GetComponent<Health>().onKill.AddListener(Revive);
	}

	public void Revive () {
		transform.position = checkpointPosition;
		onRevive.Invoke();
		GetComponent<AudioSource>().PlayOneShot(deathSND);
		GetComponent<Health>().hp = 30f;
	}

	private void Update() {
		
		onGround = GetOnGround();

		if ((hardInput.GetKeyDown("Jump") == true || hardInput.GetKeyDown("JumpAlt") == true) && onGround == true) {
			
			if (onJump != null) {
				onJump.Invoke();
			}
			GetComponent<AudioSource>().PlayOneShot(jump);
			
			//canMove = true;
			myRigidbody.AddForce(new Vector2(0, 1) * jumpImpulse, ForceMode.Impulse);

		} else if ((hardInput.GetKeyUp("Jump") == true || hardInput.GetKeyUp("JumpAlt") == true) && myRigidbody.velocity.y > 0) {

			myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y * 0.5f);
		}

		// animation
		if (hardInput.GetKeyDown("Shoot") == true) {
			if (GameManager.powerInLevel == 0) {

				GetComponent<AudioSource>().PlayOneShot(outOfAmmo);
			} else {
				GetComponent<AudioSource>().Play();
			}
		} else {

		}
		if (canMove == true) {

			if (Mathf.Abs(myRigidbody.velocity.x) > 2) {
				GetComponent<Animator>().SetBool("IsMoving", true);
			}
			else {
				GetComponent<Animator>().SetBool("IsMoving", false);
			}
		} if (canMove == false) {

			float h = hardInput.GetAxis("Right", "Left");
			if (h != 0) {
				GetComponent<Animator>().SetBool("IsMoving", true);
				if (h < 0) {
					GetComponent<SpriteRenderer>().flipX = true;
				}
				else if (h > 0) {
					GetComponent<SpriteRenderer>().flipX = false;
				}

			} else {

				GetComponent<Animator>().SetBool("IsMoving", false);
			}
		}

		GetComponent<Animator>().SetBool("InMidair", !onGround);

		// failsafe on infinite falling
		if (myRigidbody.position.y < -50) {
			Revive();
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			Revive();
		}
	}

	private void FixedUpdate () {

		float h = hardInput.GetAxis("Right", "Left");

		if (canMove == false) {
			myRigidbody.useGravity = false;
			return;
		}
		if (canMove == true && myRigidbody.useGravity == false) {
			myRigidbody.useGravity = true;
		}


		if (h != 0) {
			myRigidbody.AddForce(new Vector2(1, 0) * h * horizontalMoveForce, ForceMode.Force);
			
			if (h < 0) {
				GetComponent<SpriteRenderer>().flipX = true;
			}
			else if (h > 0) {
				GetComponent<SpriteRenderer>().flipX = false;
			}
		}
	}


	private bool GetOnGround() {

		int laymasks = (1 << GameManager.worldLayerMask) + (1 << GameManager.worldTableLayerMask);
		RaycastHit hit1, hit2;

		Physics.Raycast(myBoxCollider.bounds.center + new Vector3(-0.5f, 0), new Vector3(0, -0.6f), out hit1, 1, laymasks);
		if (hit1.collider != null) {
			return true;
		}

		Physics.Raycast(myBoxCollider.bounds.center + new Vector3(0.5f, 0), new Vector3(0, -0.6f), out hit2, 1, laymasks);
		if (hit2.collider != null) {
			return true;
		}

		return false;
	}
}
