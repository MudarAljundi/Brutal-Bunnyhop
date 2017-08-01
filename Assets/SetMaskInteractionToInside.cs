using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaskInteractionToInside : MonoBehaviour {

	// Use this for initialization
	private void Awake() {
		GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
	}
	
}
