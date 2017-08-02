using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController3D : MonoBehaviour {

	public AudioClip jumpClip;
	public float movementForce = 50;
	public float flightForce = 300;
	public float controlsStopDrag_X = 0.9f;
	public float controlsStopDrag_Y = 0.9f;
	public Vector2 normalMaxMovementVeloctity = new Vector2(15, 40);
	//public Vector2 onWallMaxMovementVeloctity = new Vector2(0, 7);
	public Vector2 flightMaxMovementVeloctity = new Vector2(10, 10);
	public Vector2 slideDashMaxMovementVeloctity = new Vector2(20, 20);

	//public Vector2 wallJumpImpulse;
	public float groundJumpImpulse;
	public bool canJump = true;

	private AudioSource playerAudioSource;
	private Rigidbody myRigidbody;
	private PlayerStats myPlayerStats;

	private void Start() {
		myRigidbody = GetComponent<Rigidbody>();
		myPlayerStats = GetComponent<PlayerStats>();
		playerAudioSource = GetComponent<AudioSource>();
	}

	private void DragVerticalMovement() {
		myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y * controlsStopDrag_Y);
	}
	private void DragHorizontalMovement() {
		myRigidbody.velocity = new Vector2(myRigidbody.velocity.x * controlsStopDrag_X, myRigidbody.velocity.y);
	}
	
	private Vector2 maxSpeed;

	private void CapVelocity() {

		maxSpeed = normalMaxMovementVeloctity;
		if (myPlayerStats.isFlight == true) {
			maxSpeed = flightMaxMovementVeloctity;
		}
		if (myPlayerStats.isSlideDashing == true) {
			maxSpeed = slideDashMaxMovementVeloctity;
		}
		//if (myPlayerStats.onWall != "None") {
		//	maxSpeed = onWallMaxMovementVeloctity;
		//}

		// cap
		myRigidbody.velocity = new Vector2(Mathf.Clamp(myRigidbody.velocity.x, -maxSpeed.x, maxSpeed.x), Mathf.Clamp(myRigidbody.velocity.y, -maxSpeed.y, maxSpeed.y));
		
	}
	

	private void StopVelocityY() {

		myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);
	}

	private float acceleration;
	private void GetVerticalFlightControls() {
		
		acceleration = movementForce;
		if (myPlayerStats.isFlight == true) {
			acceleration = flightForce;
		}
		
		float y = hardInput.GetAxis("Up", "Down") + Input.GetAxis("Vertical");

		if (y != 0) {

			myRigidbody.AddForce(y * Vector2.up * acceleration, ForceMode.Force);
		}
		else {
			// use the stopDrag only when dashing and not pressing any movement y controls!
			DragVerticalMovement();
		}

		if (y == 0) {
			y = 1;
		}
	}
	private void GetHorizontalControls() {
		
		acceleration = movementForce;
		if (myPlayerStats.isFlight == true) {
			acceleration = flightForce;
		}

		// TODO: controller or keyboard bool
		float x = hardInput.GetAxis("Right", "Left") + Input.GetAxis("Horizontal");

		if (x != 0) {

			//if (myPlayerStats.onWall == "None" || cantmove) {

			myRigidbody.AddForce(x * Vector2.right * acceleration, ForceMode.Force);

			// get orientation
			//if (lockedDirectionWhileHoldingButton == false)
			if (x > 0) {
				myPlayerStats.shootDirection = "Right";
			} else if (x < 0) {
				myPlayerStats.shootDirection = "Left";
			}
				
		} else {
			// use the stopDrag only when not pressing any movement x controls!
			DragHorizontalMovement();
		}
	}
	private void FixedUpdate() {

		if (myPlayerStats.cantMove == false) {

			GetHorizontalControls();
			if (myPlayerStats.isFlight == true) {
				GetVerticalFlightControls();
			}
		} else {
			// apply horizontal drag when dead
			DragHorizontalMovement();
		}

		CapVelocity();
	}
	
	private void Update() {
		
		if (myPlayerStats.cantMove == true) {
			return;
		}
		
		canJump = myPlayerStats.onGround;

		
		if (hardInput.GetKeyDown("Jump") == true || hardInput.GetKeyDown("Up") == true) {
			
			if (canJump == true && myPlayerStats.isFlight == false && myPlayerStats.isSlideDashing == false) {
				JumpUp();
			}

		}

		if ((hardInput.GetKeyUp("Jump") == true || hardInput.GetKeyUp("Up") == true)
			&& myPlayerStats.isFlight == false
			&& myRigidbody.velocity.y > 0) {
			
			myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y * 0.5f);
		}
	}

	public void JumpUp() {

		StopVelocityY();
		myRigidbody.AddForce(Vector2.up * groundJumpImpulse, ForceMode.Impulse);

		myPlayerStats.onGround = false;
		canJump = false;

		playerAudioSource.PlayOneShot(jumpClip);
	}

	/*
	// jump AWAY from the wall. AWAY from the direction and then let the player idle movement slide it back again
	private void WallJumpUp() {

		StartCoroutine("wallJumpingTimer");

		StopVelocityY();
		myPlayerStats.onWall = "None";

		if (myPlayerStats.shootDirection == "Left") {
			myRigidbody2D.AddForce(new Vector2(wallJumpImpulse.x, wallJumpImpulse.y), ForceMode2D.Impulse);
		} else if (myPlayerStats.shootDirection == "Right") {
			myRigidbody2D.AddForce(new Vector2(-wallJumpImpulse.x, wallJumpImpulse.y), ForceMode2D.Impulse);
		}

		playerAudioSource.RandomizePitchAndPlay(jumpClip);
	}

	IEnumerator wallJumpingTimer() {
		myPlayerStats.jumpingAwayFromWall = true;

		// suspend horizontal stop drag temporary
		float tempStopDragX = controlsStopDrag_X;
		controlsStopDrag_X = 1;

		yield return new WaitForSeconds(0.2f);
		myPlayerStats.jumpingAwayFromWall = false;

		// resume horizontal stop drag
		controlsStopDrag_X = tempStopDragX;
	}
	*/

}
