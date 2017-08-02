using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour {

	[HideInInspector]
	public List<GameObject> pooledObjects;
	
	public int objectsToCreate = 6;
	public bool setPoolTransformAsParent = false;

	public GameObject prefeb;

	// Use this for initialization
	void Awake () {
		
		pooledObjects = new List<GameObject>();

		for (int i = 0; i < objectsToCreate; i += 1) {
			MakeNewSprite ();
		}
	}
	private GameObject MakeNewSprite () {

		GameObject instance;
		
		instance = (GameObject) Instantiate (prefeb, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
		instance.SetActive (false);

		if (setPoolTransformAsParent == true) {
			instance.transform.SetParent(transform);
		}

		pooledObjects.Add (instance);

		return instance;
	}

	public GameObject Spawn (Vector3 position) {

		for (int i = 0; i < pooledObjects.Count; i += 1) {
			if (pooledObjects [i].activeSelf == false) {
				GameObject toSpawn = pooledObjects [i];

				toSpawn.transform.position = position;
				toSpawn.SetActive(true);
				
				return toSpawn;
			}
		}

		MakeNewSprite ();
		return Spawn(position);
	}

	public void DeactivateAllMembers () {
		for (int i = 0; i < pooledObjects.Count; i++) {
			pooledObjects [i].transform.position = new Vector3 (100f, float.MaxValue, 0f);
			pooledObjects [i].SetActive (false);
		}
	}
}
