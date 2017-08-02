using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

	public AudioClip damageSND;
	public AudioClip deadSND;
	public List<string> uneffectedByFactions;
	public float maxHP;
	public bool aimable = true;
	public string particleSystemExplosion = "Debris";

	public UnityEvent onDamage;
	public UnityEvent onKill;

	[HideInInspector]
	public float hp;

	// Use this for initialization
	private void Start () {
		hp = maxHP;

		if (aimable == true) {

			GameManager.shootableObjects.Add(transform);
		}
	}
	IEnumerator FlashRedCoroutine() {
		if (particleSystemExplosion == "YellowBlood") {
			GameManager.yellowBloodParticleSystem.transform.position = transform.position + new Vector3(0, 0, 1);
			GameManager.yellowBloodParticleSystem.GetComponent<ParticleSystem>().Play();
		}
		else if (particleSystemExplosion == "Blood") {
			GameManager.bloodParticleSystem.transform.position = transform.position + new Vector3(0, 0, 1);
			GameManager.bloodParticleSystem.GetComponent<ParticleSystem>().Play();
		}
		else if (particleSystemExplosion == "Debris") {
			GameManager.debrisParticleSystem.transform.position = transform.position + new Vector3(0, 0, 1);
			GameManager.debrisParticleSystem.GetComponent<ParticleSystem>().Play();
		}

		if (GetComponent<SpriteRenderer>() != null) {
			GetComponent<SpriteRenderer>().color = Color.red;
			yield return new WaitForSeconds(0.1f);
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}
	public void GiveHealth(float healAmount) {
		
		hp += healAmount;
	}
	public void TakeDamage (string faction, float damage) {

		// uneffectedByFactions
		for (int i = 0; i < uneffectedByFactions.Count; i += 1) {
			if (faction == uneffectedByFactions[i]) {
				return;
			}
		}

		hp -= damage;

		if (particleSystemExplosion == "Debris") {
			GameManager.debrisParticleSystem.transform.position = transform.position;
			GameManager.debrisParticleSystem.Play();
		}

		if (hp < 0) {
			GameManager.shootableObjects.Remove(transform);
			
			if (GetComponent<DestroyObject>()) {
				GetComponent<DestroyObject>().ExecuteDestruction();
			}
			onKill.Invoke();
		} else {

			if (damageSND != null) {
				GetComponent<AudioSource>().PlayOneShot(damageSND);
			}
			StartCoroutine(FlashRedCoroutine());
			onDamage.Invoke();
		}
		
	}
	public void TakeDamageWithPropultion(string factionName, float damageAmount, Vector2 propultion) {

		for (int i = 0; i < uneffectedByFactions.Count; i += 1) {
			if (factionName == uneffectedByFactions[i]) {
				return;
			}
		}

		if (GetComponent<Rigidbody>() == true) {
			GetComponent<Rigidbody>().AddForce(propultion, ForceMode.Impulse);
		}
		TakeDamage(factionName, damageAmount);
	}
}
