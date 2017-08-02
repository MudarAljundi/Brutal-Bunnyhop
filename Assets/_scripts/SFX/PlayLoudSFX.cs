using UnityEngine;
using System.Collections;

public class PlayLoudSFX : MonoBehaviour {

	// Use this for initialization
	public float audioSourceVolume = 1f;
	public bool playOnlyNearPlayer = true;
    public void ExecutePlayLoudSFX (AudioClip sfxToPlay) {
		if (playOnlyNearPlayer == true) {
			if (Vector2.Distance(GameManager.player.transform.position, transform.position) > 15f) {
				return;
			}
		}
		GameManager.loudAudioSource.PlayOneShot(sfxToPlay, audioSourceVolume);
	}

}