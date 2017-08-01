using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArabicSupport;

public class ArabicTranslation : MonoBehaviour {

	public Font arabicFont;
	public Vector2 newAnchoredPosition;
	public float newLineSpacing = 0.7f;
	public string newAlignment = "UpperRight";
	[TextArea(3, 10)]
	public string newText;
	
	private void Start () {
		if (LanguageManager.arabic == true) {
			GetComponent<Text>().text = ArabicFixer.Fix(newText, true, true);
			GetComponent<Text>().font = arabicFont;
			GetComponent<Text>().lineSpacing = newLineSpacing;

			if (newAnchoredPosition != Vector2.zero) {
				GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
			}

			if (newAlignment == "UpperRight") {
				GetComponent<Text>().alignment = TextAnchor.UpperRight;
			}
		}
	}
	
}
