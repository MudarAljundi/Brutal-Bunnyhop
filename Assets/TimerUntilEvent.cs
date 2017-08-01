using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerUntilEvent : MonoBehaviour {

	public float countdownTime = 0f;
	public bool executeTimerOnStart = false;
	public UnityEvent onCountdownEnd;

	// Use this for initialization
	private void Start () {
		if (executeTimerOnStart == true) {
			StartCoroutine(StartTimer());
		}
	}
	public void ExecuteTimer() {

		StartCoroutine(StartTimer());
	}


	// Update is called once per frame
	private IEnumerator StartTimer () {

		yield return new WaitForSeconds(countdownTime);

		onCountdownEnd.Invoke();
	}
}
