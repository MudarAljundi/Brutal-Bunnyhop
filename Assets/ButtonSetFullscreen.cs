using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSetFullscreen : MonoBehaviour, IPointerDownHandler {
	
	public void OnPointerDown(PointerEventData eventData) {

		// fullscreen
		Screen.fullScreen = true;
	}
}
