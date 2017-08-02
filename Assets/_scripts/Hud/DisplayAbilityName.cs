using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAbilityName : MonoBehaviour {

	public List<Sprite> graphicSprites;
	private string currentAbilityName;

	private string entityType = "Text";

	private PlayerStats myPlayerStats;

	private void Start() {
		myPlayerStats = GameManager.player.GetComponent<PlayerStats>();

		if (GetComponent<Image>()) {
			entityType = "Image";
		}
	}
	// Update is called once per frame
	private void Update () {

		if (currentAbilityName != myPlayerStats.abilityType) {
			currentAbilityName = myPlayerStats.abilityType;

			if (currentAbilityName == "Power") {
				if (entityType == "Image") {
					GetComponent<Image>().sprite = graphicSprites[0];
				} else {
					GetComponent<Text>().text = "<color=#FF0000FF>Power</color>";
				}

			} else if (currentAbilityName == "Dash") {
				
				if (entityType == "Image") {
					GetComponent<Image>().sprite = graphicSprites[1];
				}
				else {
					GetComponent<Text>().text = "<color=#169FFFFF>Dash</color>";
				}
			}
			else if(currentAbilityName == "Flight") {
				
				if (entityType == "Image") {
					GetComponent<Image>().sprite = graphicSprites[2];
				}
				else {
					GetComponent<Text>().text = "<color=#F6FF00FF>Flight</color>";
				}
			}
		}
	}
}
