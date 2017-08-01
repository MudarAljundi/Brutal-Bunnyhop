using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectOnRevive : MonoBehaviour {

	// Use this for initialization
	private void Start () {
		GameManager.player.GetComponent<MovementController>().onRevive.AddListener(ReviveThisObject);
	}
	
	// Update is called once per frame
	private void ReviveThisObject () {
		if (gameObject.activeSelf == false) {

			gameObject.SetActive(true);
			GameManager.shootableObjects.Add(transform);
		}
	}
}
