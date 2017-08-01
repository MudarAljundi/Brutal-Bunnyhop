using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeDrag : MonoBehaviour {

	public Vector2 dragXY;
	public Vector2 maxVelocity;

	private Rigidbody myRigidbody;

	// Use this for initialization
	private void Start () {
		myRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	private void FixedUpdate () {

		// max velocity
		myRigidbody.velocity = new Vector3(Mathf.Clamp(myRigidbody.velocity.x, -maxVelocity.x, maxVelocity.x),
			Mathf.Clamp(myRigidbody.velocity.y, -maxVelocity.y, maxVelocity.y));

		// drag
		myRigidbody.velocity = new Vector3(myRigidbody.velocity.x * dragXY.x, myRigidbody.velocity.y * dragXY.y);
	}
}
