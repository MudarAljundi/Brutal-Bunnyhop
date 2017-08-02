using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour {

	public float damage;
	public float propultionStrength;
	public string faction;
	public Vector2 propultionVectorOverride;
	// Use this for initialization

	private Vector3 DamageVectorNormalized(Collider collider) {

		if (propultionVectorOverride != Vector2.zero) {
			return propultionVectorOverride;
		}

		return (collider.transform.position - transform.position).normalized;
	}
	// Update is called once per frame
	private void OnCollisionEnter (Collision collision) {
		if (collision.collider.GetComponent<Health>()) {
			collision.collider.GetComponent<Health>().TakeDamageWithPropultion(faction, damage, propultionStrength * DamageVectorNormalized(collision.collider));
		}
	}
	private void OnTriggerEnter(Collider otherCollider) {
		if (otherCollider.GetComponent<Health>()) {
			otherCollider.GetComponent<Health>().TakeDamage(faction, damage);
			otherCollider.GetComponent<Health>().TakeDamageWithPropultion(faction, damage, propultionStrength * DamageVectorNormalized(otherCollider));
		}
	}
}
