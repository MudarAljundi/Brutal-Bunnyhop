using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager: MonoBehaviour {

	public static bool arabic = false;
	public bool setToArabicOnAwake;
	// Use this for initialization
	private void Awake () {
		DontDestroyOnLoad(gameObject);

		if (setToArabicOnAwake == true) {
			arabic = true;
		}
	}
	
}
