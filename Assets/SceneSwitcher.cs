using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
	
	
	// Update is called once per frame
	public void SwitchScene (string newScene) {
		SceneManager.LoadScene(newScene);
	}
}
