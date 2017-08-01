using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLightSizeBasedOnPower : MonoBehaviour {

	private Transform myTransform;

	private float savedPowerLevel;

	// Use this for initialization
	private void Start () {
		myTransform = transform;
	}

	// Update is called once per frame
	private void LateUpdate () {
		
		if (savedPowerLevel == GameManager.powerInLevel) {
			return;
		}
		savedPowerLevel = GameManager.powerInLevel;

		float size = 0.8f + (GameManager.powerInLevel * 0.003f);
		size = Mathf.Clamp(size, 0.8f, 1f);

		myTransform.localScale = new Vector3(size, size, size);
	}
}
