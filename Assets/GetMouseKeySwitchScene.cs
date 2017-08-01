using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMouseKeySwitchScene : MonoBehaviour {

	private bool canGetInput = false;

	private void Start() {
		StartCoroutine(StartGettingInput());
	}

	private IEnumerator StartGettingInput () {
		yield return new WaitForSeconds(0.15f);
		canGetInput = true;
	}

	// Update is called once per frame
	private void Update () {
		
		if (canGetInput == true && Input.GetMouseButton(0) == true) {
			GetComponent<SceneSwitcher>().SwitchScene("test1");
		}
	}
}
