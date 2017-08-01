using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {


	public float distanceToPlayer = 15f;
	public float shootDistance = 10f;
	public float weaponDamage = 10f;



	private Transform target;
	private Transform myTransform;
	private Transform lightningEnd;
	private AudioSource myAudioSource;
	private GameObject lightningLineObject;
	private LineRenderer aimLineRenderer;

	private	List<Transform> sortedTargets = new List<Transform>();

	// Use this for initialization
	private void Start () {
		myTransform = transform;
		lightningLineObject = transform.Find("LightningLine").gameObject;
		lightningEnd = transform.Find("LightningLine").transform.Find("LightningEnd");
		aimLineRenderer = transform.Find("AimLine").GetComponent<LineRenderer>();
		myAudioSource = GetComponent<AudioSource>();

		sortedTargets = GameManager.shootableObjects;
	}

	private void SetTarget() {
		
		sortedTargets.Sort(delegate (Transform a, Transform b) {
			return Vector2.Distance(myTransform.position, b.position)
			.CompareTo(Vector2.Distance(myTransform.position, a.position));
		});
		
		if (sortedTargets.Count > 0) {

			for (int i = 0; i < sortedTargets.Count; i += 1) {

				if (Vector2.Distance(transform.position, sortedTargets[i].position) < shootDistance) {

					RaycastHit hit;
					Physics.Linecast(myTransform.position, sortedTargets[i].position, out hit, (1 << GameManager.worldLayerMask) + (1 << GameManager.enemyLayerMask));

					if (hit.collider != null && hit.collider.name.StartsWith("Cube") == false) {
						target = sortedTargets[i];
					}
				}
			}
		}
	}

	IEnumerator ResetEndPosition () {

		GetComponent<SpriteRenderer>().color = Color.red;

		yield return new WaitForSeconds(0.3f);

		GetComponent<SpriteRenderer>().color = Color.white;

		if (name == "Player") {
			lightningEnd.position = myTransform.position;
		} else {
			lightningEnd.position = new Vector3(myTransform.position.x, myTransform.position.y, -0.6f);
		}

		aimLineRenderer.SetPosition(1, myTransform.InverseTransformPoint(myTransform.position));
		target = null;
	}

	private void Update () {

		if (GameManager.powerInLevel == 0 && lightningLineObject.activeSelf == true) {
			lightningLineObject.SetActive(false);
			return;
		} else if (GameManager.powerInLevel > 0 && lightningLineObject.activeSelf == false) {
			lightningLineObject.SetActive(true);
		}

		// set target
		SetTarget();

		bool targetActive;
		if (target == null) {
			targetActive = false;
		} else {
			// because of a bug, we can't destroy destroyable boxes. so we check if it's enabled
			targetActive = target.gameObject.activeSelf;
		}
		 

		if (targetActive == true) {
			aimLineRenderer.SetPosition(1, myTransform.InverseTransformPoint(target.position));
		} else {
			aimLineRenderer.SetPosition(1, myTransform.InverseTransformPoint(myTransform.position));
		}
		
		if (hardInput.GetKeyDown("Shoot") == true
			&& GameManager.powerInLevel > 0
			&& Vector2.Distance(myTransform.position, GameManager.playerTransform.position) < distanceToPlayer
			&& targetActive == true) {
			
			RaycastHit hit;
			Physics.Linecast(myTransform.position, target.position, out hit, (1 << GameManager.worldLayerMask) + (1 << GameManager.enemyLayerMask));
			
			if (hit.collider != null) {

				if (hit.collider.GetComponent<Health>()) {
					hit.collider.GetComponent<Health>().TakeDamage("LightningOrb", weaponDamage);
				}

				lightningEnd.position = new Vector3(hit.point.x, hit.point.y, -1);
				StartCoroutine(ResetEndPosition());
			}

			myAudioSource.Play();
		}
	}

}
