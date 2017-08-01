using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoToLink : MonoBehaviour, IPointerDownHandler {

	public string link;

	public void OnPointerDown(PointerEventData eventData) {
		
		if (Application.platform == RuntimePlatform.WebGLPlayer) {

			Application.ExternalEval("window.open('" + link + "');");
		} else {

			Application.OpenURL(link);
		}
	}
	
}
