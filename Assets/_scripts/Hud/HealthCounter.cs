using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour {


	private float playerHealthStartHP; //also max hp
	private float rememberPlayerHP;

	private Health playerHealth;
	private RectTransform thisRectTransform;

	private void Start() {
		playerHealth = GameManager.player.GetComponent<Health>();
		thisRectTransform = GetComponent<RectTransform>();
		playerHealthStartHP = playerHealth.maxHP;
	}

	private void Update() {

		if (rememberPlayerHP == playerHealth.hp) {
			return;
		}
		rememberPlayerHP = playerHealth.hp;
		rememberPlayerHP = Mathf.Clamp(rememberPlayerHP, 0, playerHealthStartHP);

		//GetComponent<Text>().text = Mathf.Floor(playerAbilitiesController.dashPower).ToString();
		
		// health bar is the position of the mask between 0 and 9
		thisRectTransform.anchoredPosition = new Vector3(thisRectTransform.anchoredPosition.x, rememberPlayerHP * 9f / playerHealthStartHP, 1f);
	}

	/*
	private List<GameObject> threeHearts;

	public void Update () {

		//GetComponent<Text>().text = GameManager.player.GetComponent<Health>().hp.ToString();
		float hpCounter = GameManager.player.GetComponent<Health>().hp;

		if (hpCounter > 60) {
			GetComponent<Animator>().SetInteger("HeartCount", 3);
		}
		else if (hpCounter > 30 && hpCounter <= 60) {
			GetComponent<Animator>().SetInteger("HeartCount", 2);
		}
		else if (hpCounter > 0 && hpCounter <= 30) {
			GetComponent<Animator>().SetInteger("HeartCount", 1);
		} else {
			GetComponent<Animator>().SetInteger("HeartCount", 0);
		}
	}
	*/
}
