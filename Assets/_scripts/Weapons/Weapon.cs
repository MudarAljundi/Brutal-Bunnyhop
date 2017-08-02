using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class Weapon : MonoBehaviour {
	
	[HideInInspector]
	public bool cooldown = false;

	private AudioSource audioSource;
	private Transform myTransform;

	[System.Serializable]
	public class WeaponStats {
		public string weaponName = "[insert  name  important]";
		public Vector2 targetOffsetRange = new Vector2(-0.01f, 0.01f);
		public AudioClip fireClip;
		public float gunDamage;
		public float bulletForce;
		public float prepoltionForce;
		public float cooldownTime = 0.1f;
		public float minimumHeldTime = 0.0f;
	}
	public WeaponStats weaponStats = new WeaponStats();
	


	/*
	 * I want to be able to retain weapons in between levels in final.
	 */
	private void Start() {
		myTransform = transform;
		audioSource = GetComponent<AudioSource>();
	}
	
	
	

	IEnumerator CooldownTimer() {
		cooldown = true;

		yield return new WaitForSeconds(weaponStats.cooldownTime);

		cooldown = false;
	}
	
	public Vector3 ShootHitscanWeapon(Vector2 spawnPosition, Vector2 tempShootTarget) {

		// must be any layer mask so lightning can interact with world and player.
		int layermasks = (1 << GameManager.playerLayerMask) + (1 << GameManager.worldLayerMask);
		
		RaycastHit hit;
		Physics.Linecast(myTransform.position, tempShootTarget, out hit, layermasks);

		if (hit.collider != null && hit.collider.GetComponent<Health>()) {
			hit.collider.GetComponent<Health>().TakeDamageWithPropultion("Lightning", weaponStats.gunDamage,
				(hit.collider.transform.position - myTransform.position).normalized * weaponStats.prepoltionForce);

		}

		/*
		if (hit.collider != null) {

			return hit.point;
		} else {
			// if note made it to object
			return new Ray(myTransform.position, tempShootTarget).GetPoint(15f);
		}
		*/

		// I think a linecast will always hit *something* even if shot at space, so I just return the result.
		return hit.point;
	}
	public void ShootCurrentWeapon(Vector2 spawnPosition, Vector2 tempShootTarget) {

		GameObject tempBullet = null;
		if (weaponStats.weaponName == "Vanilla") {
			
			audioSource.PlayOneShot(weaponStats.fireClip);
			tempBullet = GameManager.bulletPool.Spawn(spawnPosition);


			// bullet offset shit
			Vector2 offset = new Vector2(Random.Range(weaponStats.targetOffsetRange.x, weaponStats.targetOffsetRange.y),
								Random.Range(weaponStats.targetOffsetRange.x, weaponStats.targetOffsetRange.y));

			// neccissary bullet direction shit
			Vector2 bulletDirectionPlusOffset = (tempShootTarget - spawnPosition).normalized + offset;
			float bulletRotationDegrees = Mathf.Atan2(bulletDirectionPlusOffset.y, bulletDirectionPlusOffset.x) * Mathf.Rad2Deg;

			tempBullet.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			tempBullet.GetComponent<Rigidbody>().AddForce(bulletDirectionPlusOffset * weaponStats.bulletForce, ForceMode.Impulse);

			tempBullet.GetComponent<DamageSender>().propultionStrength = weaponStats.prepoltionForce;
			tempBullet.GetComponent<DamageSender>().damage = weaponStats.gunDamage;

			StartCoroutine(CooldownTimer());

			GameManager.gunFlashPool.Spawn(spawnPosition);
			tempBullet.transform.rotation = Quaternion.Euler(0f, 0f, bulletRotationDegrees);
		}
		else if (weaponStats.weaponName == "MetalSpike") {
			audioSource.PlayOneShot(weaponStats.fireClip);
			tempBullet = GameManager.bulletMetalPool.Spawn(spawnPosition);


			// bullet offset shit
			Vector2 offset = new Vector2(Random.Range(weaponStats.targetOffsetRange.x, weaponStats.targetOffsetRange.y),
								Random.Range(weaponStats.targetOffsetRange.x, weaponStats.targetOffsetRange.y));

			// neccissary bullet direction shit
			Vector2 bulletDirectionPlusOffset = (tempShootTarget - spawnPosition).normalized + offset;
			float bulletRotationDegrees = Mathf.Atan2(bulletDirectionPlusOffset.y, bulletDirectionPlusOffset.x) * Mathf.Rad2Deg;

			tempBullet.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			tempBullet.GetComponent<Rigidbody>().AddForce(bulletDirectionPlusOffset * weaponStats.bulletForce, ForceMode.Impulse);

			tempBullet.GetComponent<DamageSender>().propultionStrength = weaponStats.prepoltionForce;
			tempBullet.GetComponent<DamageSender>().damage = weaponStats.gunDamage;

			StartCoroutine(CooldownTimer());

			GameManager.gunFlashPool.Spawn(spawnPosition);
			tempBullet.transform.rotation = Quaternion.Euler(0f, 0f, bulletRotationDegrees);
		}
		else if (weaponStats.weaponName == "Rocket") {

			audioSource.PlayOneShot(weaponStats.fireClip);
			tempBullet = GameManager.bulletRocketPool.Spawn(spawnPosition);


			// bullet offset shit
			Vector2 offset = new Vector2(Random.Range(weaponStats.targetOffsetRange.x, weaponStats.targetOffsetRange.y),
								Random.Range(weaponStats.targetOffsetRange.x, weaponStats.targetOffsetRange.y));

			// neccissary bullet direction shit
			Vector2 bulletDirectionPlusOffset = (tempShootTarget - spawnPosition).normalized + offset;
			float bulletRotationDegrees = Mathf.Atan2(bulletDirectionPlusOffset.y, bulletDirectionPlusOffset.x) * Mathf.Rad2Deg;

			tempBullet.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			tempBullet.GetComponent<Rigidbody>().AddForce(bulletDirectionPlusOffset * weaponStats.bulletForce, ForceMode.Impulse);

			//tempBullet.GetComponent<DamagerSender>().propultionStrength = weaponStats.prepoltionForce;
			//tempBullet.GetComponent<DamagerSender>().hpDamage = weaponStats.gunDamage;

			StartCoroutine(CooldownTimer());

			GameManager.gunFlashPool.Spawn(spawnPosition);
			tempBullet.transform.rotation = Quaternion.Euler(0f, 0f, bulletRotationDegrees);
		}

	}
	/*
	 * // Might put this in a separate script for hitscan weapons.
	 * 
	private GameObject playerElectricTransform;
	public void PlayerElectricShoot(Vector3 spawnPosition, Vector2 shootTarget) {
		if (canShootGun == true) {
			canShootGun = false;
			SpawnBullet(gunName, spawnPosition, shootTarget);
			StartCoroutine("CooldownTimer");
			StartCoroutine("StopElectricBeam");

			Physi
		}
	}
	IEnumerator StopElectricBeam() {
		yield return new WaitForSeconds(1f);
		GetComponent<>
	}
	*/

	/*
	// it's probably a better idea to put the x3 fire in a player shoot script because of the target. It needs to be the most recent target.
	
	public void SemiAutoShoot(Vector3 spawnPosition, Vector2 shootTarget) {
		if (canShootGun == true) {
			canShootGun = false;
			StartCoroutine("ReloadTimer");
			StartCoroutine(SecondBulletTimer());
			StartCoroutine(ThirdBulletTimer());
			SpawnBullet(spawnPosition, shootTarget);
		}
	}
	IEnumerator SecondBulletTimer() {
		yield return new WaitForSeconds(semiAutoTime);
		SpawnBullet(spawnPosition, shootTarget);
	}
	IEnumerator ThirdBulletTimer() {
		yield return new WaitForSeconds(semiAutoTime * 2);
		SpawnBullet(spawnPosition, shootTarget);
	}
	*/
	/*
	public void SpawnRocket(Vector2 spawnPosition, Vector2 shootTarget) {

		GameObject tempRocket = GameManager.rocketPool.Spawn(spawnPosition);
		Vector2 rocketDirection = (shootTarget - spawnPosition).normalized;

		//tempRocket.GetComponent<ObjectFixedUpdateTranslate>().SetTranslateVector(bulletDirection);
		tempRocket.GetComponent<Rigidbody2D>().AddForce(rocketDirection * shootForce, ForceMode2D.Impulse);

		GameManager.loadAudioSource.PlayOneShot(rocketLauncherClip);
	}
	*/

}
