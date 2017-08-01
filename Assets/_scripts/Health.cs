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
	private IEnumerator FlashRed () {

		Color defaultColor = GetComponent<SpriteRenderer>().color;
		GetComponent<SpriteRenderer>().color = Color.red;

		yield return new WaitForSeconds(0.1f);

		GetComponent<SpriteRenderer>().color = defaultColor;
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
			onKill.Invoke();
			GameManager.shootableObjects.Remove(transform);

			//if (deadSND != null) {
			//	GameManager.loudAudioSource.PlayOneShot(deadSND);
			//}

			if (GetComponent<DestroyObject>()) {
				GetComponent<DestroyObject>().ExecuteDestruction();
			}
			return;
		}
		
		if (damageSND != null) {
			GetComponent<AudioSource>().PlayOneShot(damageSND);
		}
		StartCoroutine(FlashRed());
		onDamage.Invoke();
	}
}
