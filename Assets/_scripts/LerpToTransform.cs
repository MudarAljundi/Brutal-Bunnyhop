using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToTransform : MonoBehaviour {

	public Transform targetTransform;
	public Vector3 offsetPosition;
	public float lerpSpeed;

	private Transform myTransform;

	// Use this for initialization


	private void Start() {
		myTransform = transform;
	}

	private void Reset () {
		targetTransform = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	private void LateUpdate () {
		myTransform.position = Vector3.Lerp(myTransform.position, targetTransform.position + offsetPosition, lerpSpeed);
	}
}
