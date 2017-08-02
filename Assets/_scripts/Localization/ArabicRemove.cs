using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArabicSupport;

public class ArabicRemove : MonoBehaviour {

	public bool removeInArabic = true;
	public bool removeInEnglish = true;

	private void Start () {

		if (removeInArabic == true && LanguageManager.arabic == true) {

			Destroy(gameObject);
		}
		else
		if (removeInEnglish == true && LanguageManager.arabic == false) {

			Destroy(gameObject);
		}
	}
	
}
