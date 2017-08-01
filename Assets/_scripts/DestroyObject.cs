using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

	public string destroyOrDeactivate = "Deactivate";

	// Update is called once per frame
	public void ExecuteDestruction () {
		if (destroyOrDeactivate == "Deactivate") {
			gameObject.SetActive(false);
		} else if (destroyOrDeactivate == "Destroy") {
			Destroy(gameObject);
		}
	}
}
