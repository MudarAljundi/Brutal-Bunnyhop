using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform targetTransform;
	public Vector3 offsetPosition;
	public float lerpSpeed;

	private Transform myTransform;

	private Bounds cameraZoneBounds;

	// Use this for initialization


	private void Start() {
		myTransform = transform;

		cameraZoneBounds = GameObject.Find("CameraZone").GetComponent<BoxCollider2D>().bounds;
	}

	private void Reset () {
		targetTransform = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	private void LateUpdate () {
		
		myTransform.position = Vector3.Lerp(myTransform.position, targetTransform.position + offsetPosition, lerpSpeed);
		
		// keep in bounds
		Vector2 closestCameraPointInBounds = cameraZoneBounds.ClosestPoint(myTransform.position);
		
		myTransform.position = new Vector3(closestCameraPointInBounds.x, closestCameraPointInBounds.y, offsetPosition.z);

	}
}
