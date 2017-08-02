using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectOnPlayerDeath : MonoBehaviour {

	private Vector3 startingPosition;
	private float startingHealth;

	private void Start () {
		GameManager.player.GetComponent<PlayerStats>().resetObjectsOnDeath.Add(transform);

		startingPosition = transform.position;
	}
	
	public void ResetState() {

		transform.position = startingPosition;

		if (GetComponent<Health>()) {
			GetComponent<Health>().hp = GetComponent<Health>().maxHP;
		}
	}
}
