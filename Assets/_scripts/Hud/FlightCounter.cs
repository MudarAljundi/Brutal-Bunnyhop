using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlightCounter: MonoBehaviour {

	public float maxValue = 100f;
	private PlayerDashController3D playerDashController3D;

	private void Start() {
		playerDashController3D = GameManager.player.GetComponent<PlayerDashController3D>();
	}

	private float currentFlightPower;
	private void Update () {

		if (currentFlightPower == playerDashController3D.flightPower) {
			return;
		}
		currentFlightPower = playerDashController3D.flightPower;

		//GetComponent<Text>().text = Mathf.Floor(playerAbilitiesController.dashPower).ToString();
		transform.localScale = new Vector3(currentFlightPower * 100f / maxValue, transform.localScale.y, 1f);
	}
	
}
