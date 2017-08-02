using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevCounter : MonoBehaviour {

	public int chamberIndex = 0;
	private string chamberState;
	
	private PlayerWeaponController playerWeapons;

	private void Start() {
		playerWeapons = GameManager.player.GetComponent<PlayerWeaponController>();
		
		chamberState = playerWeapons.bulletsInChamber[chamberIndex];
	}

	private void Update() {
		
		if (playerWeapons.bulletsInChamber[chamberIndex] == chamberState) {
			return;
		}
		chamberState = playerWeapons.bulletsInChamber[chamberIndex];
		
		if (chamberState == "Empty") {
			GetComponent<Image>().color = Color.black;
		} else if (chamberState == "Vanilla") {
			GetComponent<Image>().color = Color.white;
		}
		else if (chamberState == "Rocket") {
			GetComponent<Image>().color = Color.red;
		}

	}

}
