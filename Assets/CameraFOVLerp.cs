using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOVLerp : MonoBehaviour {

	[HideInInspector]
	public float defaultFOVSize;

	private Camera myCamera;

	private bool executingLerp;
	private float targetFOV;
	private float rate;

	public void FOVSetTo(float newFOV) {
		myCamera.fieldOfView = newFOV;
	}

	public void FOVLerpTo(float newTargetFOV, float newRate) {

		executingLerp = true;
		targetFOV = newTargetFOV;
		rate = newRate;
	}

	private void Start () {
		defaultFOVSize = Camera.main.fieldOfView;
		myCamera = GetComponent<Camera>();
	}

	// Update is called once per frame
	private void Update () {
		
		if (executingLerp == true) {

			if (Mathf.Abs(myCamera.fieldOfView - targetFOV) < Mathf.Epsilon) {
				executingLerp = false;
			}

			myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetFOV, rate);
		}
	}
}
