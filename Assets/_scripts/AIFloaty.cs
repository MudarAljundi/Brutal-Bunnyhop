using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFloaty : MonoBehaviour {

	public float distanceToMoveToPlayer;
	public float moveToPlayerImpulse;
	
	private Rigidbody myRigidbody;
	private Transform myTransform;

	private void Start () {
		myRigidbody = GetComponent<Rigidbody>();
		myTransform = transform;

		StartCoroutine(MoveToPlayerUpdate());
	}
	
	/*
	private void Update () {
		
	}
	*/
	private IEnumerator MoveToPlayerUpdate() {
		
		yield return new WaitForSeconds(0.5f);

		if (Vector2.Distance(GameManager.playerTransform.position, myTransform.position) < distanceToMoveToPlayer) {
			// direction
			Vector3 targetVector = (GameManager.playerTransform.position - myTransform.position).normalized;
			// offset
			targetVector = new Vector3(targetVector.x + Random.Range(-0.02f, 0.02f), targetVector.y + Random.Range(-0.02f, 0.02f));

			myRigidbody.AddForce(targetVector * moveToPlayerImpulse, ForceMode.Impulse);

			if (Random.Range(0, 100) > 90) {
				GetComponent<AudioSource>().Play();
			}
		}
		StartCoroutine(MoveToPlayerUpdate());
	}
}
