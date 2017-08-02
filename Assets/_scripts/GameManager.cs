using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour {

	public static bool pausedGame = false;

	public static float powerInLevel = 70;

	public static int playerLayerMask;
	public static int worldLayerMask;
	public static int enemyLayerMask;
	public static int worldTableLayerMask;

	public static GameObject gameManagerGameObject;
	public static AudioSource loudAudioSource;

	public static GameObject player;
	public static Transform playerTransform;
	public static GameObject hud;

	public static ParticleSystem debrisParticleSystem;
	public static ParticleSystem bloodParticleSystem;
	public static ParticleSystem yellowBloodParticleSystem;

	public static Pool bulletPool;
	public static Pool bulletMetalPool;
	public static Pool bulletRocketPool;
	public static Pool explosionPool;
	public static Pool gunFlashPool;
	

	public static List<Transform> shootableObjects = new List<Transform>();

	public AudioMixer masterMixer;

	// Use this for initialization
	private void Start () {
		playerLayerMask = LayerMask.NameToLayer("Player");
		worldLayerMask = LayerMask.NameToLayer("World");
		enemyLayerMask = LayerMask.NameToLayer("Enemy");
		worldTableLayerMask = LayerMask.NameToLayer("WorldTable");

		gameManagerGameObject = gameObject;

		player = GameObject.Find("Player");
		loudAudioSource = GameObject.Find("Meta/LoudAudio").GetComponent<AudioSource>();
		playerTransform = GameObject.Find("Player").transform;
		hud = transform.Find("HUD").gameObject;

		debrisParticleSystem = transform.Find("DebrisParticleSystem").GetComponent<ParticleSystem>();
		bloodParticleSystem = transform.Find("BloodParticleSystem").GetComponent<ParticleSystem>();
		yellowBloodParticleSystem = transform.Find("YellowParticleSystem").GetComponent<ParticleSystem>();

		bulletPool = transform.Find("Pool/BulletPool").GetComponent<Pool>();
		bulletMetalPool = transform.Find("Pool/BulletMetalPool").GetComponent<Pool>();
		bulletRocketPool = transform.Find("Pool/BulletRocketPool").GetComponent<Pool>();
		explosionPool = transform.Find("Pool/ExplosionPool").GetComponent<Pool>();
		gunFlashPool = transform.Find("Pool/GunFlashPool").GetComponent<Pool>();
	}
	public void SetVolume(float value) {
		masterMixer.SetFloat("SFX", value);
	}

	public void TogglePauseGame() {

		pausedGame = !pausedGame;

		if (pausedGame == true) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}

	}

	private void LateUpdate() {

		// a timer would be too confusing anyway
		//powerInLevel -= 1 * Time.deltaTime;
		powerInLevel = Mathf.Clamp(powerInLevel, 0, 70);
		
		if (hardInput.GetKeyDown("Power")) {
			powerInLevel -= 10;
			/*
			if (powerInLevel < 70 && powerInLevel > 60) {
				powerInLevel = 60;
			} else if (powerInLevel < 60 && powerInLevel > 50) {
				powerInLevel = 50;
			}
			else if (powerInLevel < 50 && powerInLevel > 40) {
				powerInLevel = 40;
			}
			else if (powerInLevel < 40 && powerInLevel > 30) {
				powerInLevel = 30;
			}
			else if (powerInLevel < 30 && powerInLevel > 20) {
				powerInLevel = 20;
			}
			else if (powerInLevel < 20 && powerInLevel > 10) {
				powerInLevel = 10;
			}
			else if (powerInLevel < 10 && powerInLevel > 0) {
				powerInLevel = 0;
			}
			*/
		}
	}
}
