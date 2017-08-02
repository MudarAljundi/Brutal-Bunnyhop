using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueText : MonoBehaviour {

	public float removeSelfTimer = 15f;
	public float letterCountdown = 0.1f;
	[TextArea(3, 10)]
	public string dialogueText;

	private bool imageIsTalking = false;
	private float punctuationPauseCountdown = 0f;
	private bool isWritingDialogue = false;
	private float internalLetterTimer;
	private int currentIndex;
	

	private void Update () {
		if (isWritingDialogue == false) {
			return;
		}
		if (dialogueText.Length <= 0) {
			isWritingDialogue = false;
		}
		// We don't use IEnumorators;
		internalLetterTimer += Time.deltaTime;
		
		if (internalLetterTimer > letterCountdown + punctuationPauseCountdown) {
			
			if (dialogueText.Length == 0) {
				isWritingDialogue = false;
				imageIsTalking = false;
				return;
			}

			DisplayFirstLetter();
			internalLetterTimer = internalLetterTimer - (letterCountdown + punctuationPauseCountdown); // reset our timer to 0 plus leftover from last countdown
			punctuationPauseCountdown = 0f;
		}

		if (internalLetterTimer > removeSelfTimer) {

			transform.parent.GetComponent<EntityFade>().StartFadeOut();
		}
	}
	
	public void StartDialogueScene() {
		internalLetterTimer = 0;
		GetComponent<Text>().text = "";
		isWritingDialogue = true;

		transform.parent.GetComponent<EntityFade>().StartFadeIn();
	} 
	private void DisplayFirstLetter () {

		if (dialogueText.StartsWith("[talk=")) {
			if (dialogueText.Substring(7, dialogueText.Length).StartsWith("overseer-normal]")) {
				//print ();
			}
		}
		/*
		if (dialogueText.StartsWith(",")) {
			punctuationPauseCountdown = 1f;
			imageIsTalking = false;
		}
		if (dialogueText.StartsWith(";")) {
		
			punctuationPauseCountdown = 1f;
			imageIsTalking = false;
		}
		if (dialogueText.StartsWith("!")) {
		
			punctuationPauseCountdown = 2f;
			imageIsTalking = false;
		}
		if (dialogueText.StartsWith("?")) {
		
			punctuationPauseCountdown = 2f;
			imageIsTalking = false;
		}
		if (dialogueText.StartsWith(".")) {
			punctuationPauseCountdown = 1.5f;
			
			imageIsTalking = false;
		}
		if (dialogueText.StartsWith("[wait=")) {
		
			punctuationPauseCountdown = ;
			imageIsTalking = false;
		}
		if (dialogueText.StartsWith("[lcd=")) {
		
			letterCountdown = 0.1f;
		}
		if (dialogueText.StartsWith("[snd=")) {
			GetComponent<AudioSource>().PlayOneShot();
		}
		*/

		GetComponent<Text>().text = GetComponent<Text>().text + dialogueText[0];
		dialogueText = dialogueText.Remove(0, 1);

		imageIsTalking = true;
		currentIndex += 1;
		if (currentIndex % 2 == 0) {
			GetComponent<AudioSource>().Play();
		}
	}
}
