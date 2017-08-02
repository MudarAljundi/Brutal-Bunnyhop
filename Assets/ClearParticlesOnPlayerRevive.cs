using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearParticlesOnPlayerRevive : MonoBehaviour {

	// Use this for initialization
	private void Start () {
		GameManager.player.GetComponent<PlayerStats>().OnPlayerRevive.AddListener(ClearParticlesOnMap);
	}
	
	// Update is called once per frame
	private void ClearParticlesOnMap() {
		GetComponent<ParticleSystem>().Clear();
	}
}
