using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonToggleFullscreen : MonoBehaviour, IPointerDownHandler {
	
	public void OnPointerDown(PointerEventData eventData) {
		
		// fullscreen
		Screen.fullScreen = !Screen.fullScreen;
	}

	// Update is called once per frame
	//public void ToggleFullscreen() {
	//	Screen.fullScreen = !Screen.fullScreen;
	//}
}
