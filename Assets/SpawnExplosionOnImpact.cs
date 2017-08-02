using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplosionOnImpact : MonoBehaviour {
	
	private void OnTriggerEnter (Collider collider) {

		GameManager.explosionPool.Spawn(transform.position);

		GetComponent<DestroyObject>().ExecuteDestruction();
	}

	private void OnCollisionEnter (Collision collision) {

		GameManager.explosionPool.Spawn(transform.position);

		GetComponent<DestroyObject>().ExecuteDestruction();
	}
}
