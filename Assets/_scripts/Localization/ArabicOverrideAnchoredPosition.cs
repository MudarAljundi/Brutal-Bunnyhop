using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArabicSupport;

public class ArabicOverrideAnchoredPosition: MonoBehaviour {
	
	public Vector2 newAnchoredPosition;
	
	private void Start () {
		if (LanguageManager.arabic == true) {
			
			GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
		}
	}
	
}
