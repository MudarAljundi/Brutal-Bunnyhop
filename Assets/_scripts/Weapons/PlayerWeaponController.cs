using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class PlayerWeaponController : MonoBehaviour {

	public Dictionary<int, string> bulletsInChamber;
	
	public float reloadChamberTime = 0.2f;
	public float initialReloadTime = 0.6f;
	
	private bool startReloadingVanilla = false;

	public AudioClip loadChamberClip;
	private Transform bulletStartTransform;
	private AudioSource playerAudioSource;
	private PlayerStats myPlayerStats;
	private PlayerDashController3D playerDashController3D;
	private Weapon weapon;

	[System.Serializable]
	public class VanillaAmmo {
		public string weaponName = "[insert  name  important]";
		public Vector2 targetOffsetRange = new Vector2(-0.01f, 0.01f);
		public AudioClip fireClip;
		public float gunDamage;
		public float bulletForce;
		public float prepoltionForce;
		public float cooldownTime = 0.1f;
		public float minimumHeldTime = 0.0f;
		public GameObject lightningGameObject;
	}
	public VanillaAmmo vanillaAmmo = new VanillaAmmo();

	[System.Serializable]
	public class RocketAmmo {
		public string weaponName = "[insert  name  important]";
		public Vector2 targetOffsetRange = new Vector2(-0.01f, 0.01f);
		public AudioClip fireClip;
		public float gunDamage;
		public float bulletForce;
		public float prepoltionForce;
		public float cooldownTime = 0.1f;
		public float minimumHeldTime = 0.0f;
		public GameObject lightningGameObject;
	}
	public RocketAmmo rocketAmmo = new RocketAmmo();

	private void Awake() {
		bulletStartTransform = transform.Find("GunStart");

		weapon = GetComponent<Weapon>();
		playerAudioSource = GetComponent<AudioSource>();
		myPlayerStats = GetComponent<PlayerStats>();
		playerDashController3D = GetComponent<PlayerDashController3D>();

		bulletsInChamber = new Dictionary<int, string>();
		bulletsInChamber.Add(0, "Vanilla");
		bulletsInChamber.Add(1, "Vanilla");
		bulletsInChamber.Add(2, "Vanilla");
		bulletsInChamber.Add(3, "Vanilla");
		bulletsInChamber.Add(4, "Vanilla");
		bulletsInChamber.Add(5, "Vanilla");
	}

	public void ResetBulletsInChamber() {
		
		for (int i = 0; i < 6; i++) {
			bulletsInChamber[i] = "Vanilla";
		}
	}
	public bool ChambersAreFull() {

		return bulletsInChamber[0] != "Empty";
	}
	public bool ChambersAreEmpty() {

		for (int i = 0; i < 6; i++) {
			if (bulletsInChamber[i] != "Empty") {

				return false;
			}
		}

		return true;
	}
	public string GetShootChamber() {

		for (int i = 0; i < 6; i++) {
			if (bulletsInChamber[i] != "Empty") {

				return bulletsInChamber[i];
			}
		}

		return "Empty";
	}

	private void PowerShootCheck() {
		
		if (weapon.cooldown == true || ChambersAreEmpty() == true) {
			return;
		}

		// order matters

		playerDashController3D.ChangeAbility("Power");
		Shoot();
		EmptyFirstChamber();
		weapon.cooldown = true;
	}

	/* 
	 * Cooldown is when no bullet can happen even if the fire button is pressed/held/unpressed.
	 * ideally, I want all guns to be usable while HOLDING the fire key, with better efficiancy than tapping the key.
	 */
	private void Update() {
		
		if (myPlayerStats.cantMove == true) {
			startReloadingVanilla = false;
			return;
		}

		if (hardInput.GetKey("Power") == true && ChambersAreEmpty() == false) {

			PowerShootCheck();
			startReloadingVanilla = true;
		}

		if (startReloadingVanilla == true && ChambersAreFull() == false) {

			startReloadingVanilla = false;
			StopCoroutine("InitialVanillaReloadCountdown");
			StopCoroutine("VanillaReloadCountdown");

			StartCoroutine("InitialVanillaReloadCountdown");
		}
	}
	

	public void ChangeWeaponStatsToCurrentWeapon() {

		weapon.weaponStats.weaponName = GetShootChamber();

		if (weapon.weaponStats.weaponName == "Vanilla") {

			weapon.weaponStats.targetOffsetRange = vanillaAmmo.targetOffsetRange;
			weapon.weaponStats.fireClip = vanillaAmmo.fireClip;
			weapon.weaponStats.gunDamage = vanillaAmmo.gunDamage;
			weapon.weaponStats.bulletForce = vanillaAmmo.bulletForce;
			weapon.weaponStats.prepoltionForce = vanillaAmmo.prepoltionForce;
			weapon.weaponStats.cooldownTime = vanillaAmmo.cooldownTime;
			weapon.weaponStats.minimumHeldTime = vanillaAmmo.minimumHeldTime;
		}
		if (weapon.weaponStats.weaponName == "Rocket") {

			weapon.weaponStats.targetOffsetRange = rocketAmmo.targetOffsetRange;
			weapon.weaponStats.fireClip = rocketAmmo.fireClip;
			weapon.weaponStats.gunDamage = rocketAmmo.gunDamage;
			weapon.weaponStats.bulletForce = rocketAmmo.bulletForce;
			weapon.weaponStats.prepoltionForce = rocketAmmo.prepoltionForce;
			weapon.weaponStats.cooldownTime = rocketAmmo.cooldownTime;
			weapon.weaponStats.minimumHeldTime = rocketAmmo.minimumHeldTime;
		}
	}
	public void Shoot() {

		if (weapon.weaponStats.weaponName != GetShootChamber()) {
			ChangeWeaponStatsToCurrentWeapon();
		}

		print(bulletStartTransform.position);
		weapon.ShootCurrentWeapon(bulletStartTransform.position, myPlayerStats.shootTarget);
	}

	/*
		DestroyFirstBullet()

		n n n n n n
		becomes
		- n n n n n

		- n n n n n n
		becomes
		- - n n n n n
	*/
	public void EmptyFirstChamber() {

		for (int i = 0; i < 6; i++) {

			if (bulletsInChamber[i] != "Empty") {
				bulletsInChamber[i] = "Empty";
				return;
			}
		}
	}
	/*
		AddBullet() pushes a new bullet in the queue

		- - - n n n
		becomes
		- - N n n n

		n n n n n n n
		becomes
		N n n n n n n
		
		N N n n n n n
		becomes
		N N N n n n n
	*/
	public void AddBullet(string newBulletType) {

		// push the new bullet at the end. Starting from the LAST chamber
		for (int i = 5; i >= 0; i -= 1) {
			if (bulletsInChamber[i] == "Empty") {
				bulletsInChamber[i] = newBulletType;
				return;
			}
		}

		// else, all chambers full
		// push the new bullet if all bullet types are normal. Starting from the firing chamber
		if (newBulletType != "Vanilla") {

			for (int i = 0; i < 5; i += 1) {
				if (bulletsInChamber[i] == "Vanilla") {
					bulletsInChamber[i] = newBulletType;
					return;
				}
			}
		}

	}
	private void LoadFirstEmptyChamber() {

		playerAudioSource.PlayOneShot(loadChamberClip);

		AddBullet("Vanilla");
	}

	IEnumerator InitialVanillaReloadCountdown() {

		yield return new WaitForSeconds(initialReloadTime);

		StartCoroutine("VanillaReloadCountdown");

	}
	IEnumerator VanillaReloadCountdown() {
		yield return new WaitForSeconds(reloadChamberTime);

		LoadFirstEmptyChamber();
		
		// if (bulletsInChamber[5] == "Empty" && myPlayerStats.cantMove == false) {
		if (ChambersAreFull() == false) {
			StartCoroutine("VanillaReloadCountdown");
		}
	}
}
