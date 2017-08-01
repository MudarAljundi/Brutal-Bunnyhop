using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour {

	public bool arabic = false;

	public static float powerInLevel = 70;

	public static int playerLayerMask;
	public static int worldLayerMask;
	public static int enemyLayerMask;
	public static int worldTableLayerMask;

	public static GameObject gameManagerGameObject;
	public static AudioSource treadmillAudioSource;
	public static AudioSource loudAudioSource;

	public static GameObject player;
	public static Transform playerTransform;

	public static ParticleSystem debrisParticleSystem;
	
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
		treadmillAudioSource = GameObject.Find("Meta/TreadmillAudio").GetComponent<AudioSource>();
		loudAudioSource = GameObject.Find("Meta/LoudAudio").GetComponent<AudioSource>();
		playerTransform = GameObject.Find("Player").transform;

		debrisParticleSystem = transform.Find("DebrisParticleSystem").GetComponent<ParticleSystem>();
	}
	public void SetVolume(float value) {
		masterMixer.SetFloat("SFX", value);
	}

	private void LateUpdate() {

		// a timer would be too confusing anyway
		//powerInLevel -= 1 * Time.deltaTime;
		powerInLevel = Mathf.Clamp(powerInLevel, 0, 70);
		
		if (hardInput.GetKeyDown("Shoot")) {
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
