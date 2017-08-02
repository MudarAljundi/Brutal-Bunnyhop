using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockonTarget : MonoBehaviour {
	public float smoothTime = 0.2f;

	private Vector3 actualGoTo;

	private Transform thisRectTransform;
	private PlayerStats playerStats;
	private Camera mainCamera;

	// Use this for initialization
	private void Start() {
		thisRectTransform = GetComponent<RectTransform>();
		playerStats = GameManager.player.GetComponent<PlayerStats>();
		mainCamera = Camera.main;
	}

	private void LateUpdate() {
		Vector3 target = mainCamera.WorldToScreenPoint(playerStats.shootTarget);

		thisRectTransform.position = Vector3.LerpUnclamped(thisRectTransform.position, target, smoothTime);
	}
	
}
