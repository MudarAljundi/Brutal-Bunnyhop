using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour {

	public float damage;
	public string faction;
	// Use this for initialization
	
	// Update is called once per frame
	private void OnCollisionEnter (Collision collision) {
		if (collision.collider.GetComponent<Health>()) {
			collision.collider.GetComponent<Health>().TakeDamage(faction, damage);
		}
	}
	private void OnTriggerEnter(Collider collider) {
		if (collider.GetComponent<Health>()) {
			collider.GetComponent<Health>().TakeDamage(faction, damage);
		}
	}
}
