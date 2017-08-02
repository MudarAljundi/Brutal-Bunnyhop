using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRemoveSpriteRenderer : MonoBehaviour {

	private void Start () {
		GameObject.Destroy(GetComponent<SpriteRenderer>());
		GameObject.Destroy(GetComponent<DebugRemoveSpriteRenderer>());
	}
}
