using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup: MonoBehaviour {

	public AudioClip ammoInsertSFX;
	public string thisAmmoType;
	public float healHP = 25f;
	
	private Transform revTarget;
	private Vector3 revTargetPosition;

	private bool pickedUp = false;

	private void Start() {
		revTarget = GameManager.hud.transform.Find("RevChamber");
	}

	public void PickupThisAmmo() {

		if (pickedUp == false) {
			pickedUp = true;

			revTargetPosition = revTarget.position;
			GameManager.loudAudioSource.PlayOneShot(ammoInsertSFX);
			GameManager.player.GetComponent<PlayerWeaponController>().AddBullet(thisAmmoType);
			GameManager.player.GetComponent<Health>().GiveHealth(healHP);
			StartCoroutine(RemoveCountdown());
		}
	}

	private void Update() {
		if (pickedUp == true) {
			transform.position = Vector3.Lerp(transform.position, revTargetPosition, 0.5f);
		}
	}
	IEnumerator RemoveCountdown () {
		yield return new WaitForSeconds(0.3f);

		GetComponent<DestroyObject>().ExecuteDestruction();
	}
}