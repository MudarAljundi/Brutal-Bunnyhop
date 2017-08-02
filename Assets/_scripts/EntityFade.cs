using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EntityFade : MonoBehaviour {


	public bool disableAfterFadeOut = true;

	public float speedMultiplier = 0.5f;
	private string objectType = "Sprite";

	private Color defaultColor;

	private void OnEnable() {
		if (GetComponent<SpriteRenderer>()) {
			objectType = "Sprite";
			defaultColor = GetComponent<SpriteRenderer>().color;
		}
		else if (GetComponent<Image>()) {
			objectType = "UI";
			defaultColor = GetComponent<Image>().color;
		}
		else if (GetComponent<Text>()) {
			objectType = "Text";
			defaultColor = GetComponent<Text>().color;
		}
		else if (GetComponent<CanvasGroup>()) {
			objectType = "CanvasGroup";
		}
	}

	public void EnableGraphicalComponent() {
		
		if (objectType == "Sprite") {
			GetComponent<SpriteRenderer>().enabled = true;
		}
		else if (objectType == "UI") {
			GetComponent<Image>().enabled = true;
		}
		else if (objectType == "Text") {
			GetComponent<Text>().enabled = true;
		}
		//else if (objectType == "CanvasGroup") {
		//	GetComponent<CanvasGroup>().alpha = 1.0f;
		//}
	}
	public void SetAlphaToValue(float value) {
		
		if (objectType == "Sprite") {
			Color c = GetComponent<SpriteRenderer>().color;
			c.a = value;
			GetComponent<SpriteRenderer>().color = c;
		}
		else if (objectType == "UI") {
			Color c = GetComponent<Image>().color;
			c.a = value;
			GetComponent<Image>().color = c;
		}
		else if (objectType == "Text") {
			Color c = GetComponent<Text>().color;
			c.a = value;
			GetComponent<Text>().color = c;
		}
		else if (objectType == "CanvasGroup") {
			GetComponent<CanvasGroup>().alpha = value;
		}
	}

	// can be shotened but jesus who cares?
	public void StartFadeIn (float newSpeedMuilt = 0.5f) {

		speedMultiplier = newSpeedMuilt;

		gameObject.SetActive (true);
		EnableGraphicalComponent();

		StartCoroutine("FadeIn");
	}

	public void StartFadeOut(float newSpeedMuilt = 0.5f) {

		speedMultiplier = newSpeedMuilt;

		gameObject.SetActive(true);
		EnableGraphicalComponent();

		StartCoroutine("FadeOut");
	}

	IEnumerator FadeIn () {
		for (float f = 0f; f < 1; f += Time.deltaTime * speedMultiplier) {
			if (objectType == "Sprite") {
				Color c = GetComponent<SpriteRenderer>().color;
				c.a = f;
				GetComponent<SpriteRenderer>().color = c;
			}
			else if (objectType == "UI") {
				Color c = GetComponent<Image>().color;
				c.a = f;
				GetComponent<Image>().color = c;
			}
			else if (objectType == "Text") {
				Color c = GetComponent<Text>().color;
				c.a = f;
				GetComponent<Text>().color = c;
			}
			else if (objectType == "CanvasGroup") {
				GetComponent<CanvasGroup>().alpha = f;
			}

			yield return null;
		}
		// after loop: result
		SetAlphaToValue(1f);
	}
	IEnumerator FadeOut () {
		for (float f = 1f; f >= 0; f -= Time.deltaTime * speedMultiplier) {

			if (objectType == "Sprite") {
				Color c = GetComponent<SpriteRenderer>().color;
				c.a = f;
				GetComponent<SpriteRenderer>().color = c;
			}
			else if (objectType == "UI") {
				Color c = GetComponent<Image>().color;
				c.a = f;
				GetComponent<Image>().color = c;
			}
			else if (objectType == "Text") {
				Color c = GetComponent<Text>().color;
				c.a = f;
				GetComponent<Text>().color = c;
			}
			else if (objectType == "CanvasGroup") {
				GetComponent<CanvasGroup>().alpha = f;
			}

			yield return null;
		}

		//after loop
		SetAlphaToValue(0f);
		if (disableAfterFadeOut) {
			gameObject.SetActive (false);
			
			if (objectType == "Sprite") {
				GetComponent<SpriteRenderer>().color = defaultColor;
			}
			else if (objectType == "UI") {
				GetComponent<Image>().color = defaultColor;
			}
			else if (objectType == "Text") {
				GetComponent<Text>().color = defaultColor;
			}
				
		}
	}
}
