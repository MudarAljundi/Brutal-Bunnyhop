using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGraphicManager : MonoBehaviour {

	public List<Transform> cells;

	private int currentIndex = 8;

	// Update is called once per frame
	private void Update () {
		
		if (currentIndex != 7 && GameManager.powerInLevel <= 70 && GameManager.powerInLevel > 60) {
			currentIndex = 7;
			cells[0].GetComponent<Animator>().SetBool("Empty", false);
			cells[1].GetComponent<Animator>().SetBool("Empty", false);
			cells[2].GetComponent<Animator>().SetBool("Empty", false);
			cells[3].GetComponent<Animator>().SetBool("Empty", false);
			cells[4].GetComponent<Animator>().SetBool("Empty", false);
			cells[5].GetComponent<Animator>().SetBool("Empty", false);
			cells[6].GetComponent<Animator>().SetBool("Empty", false);

			cells[0].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[1].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[2].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[3].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[4].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[5].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[6].GetComponent<Animator>().SetBool("CurrentAnimation", true);
		}
		else if (currentIndex != 6 && GameManager.powerInLevel <= 60 && GameManager.powerInLevel > 50) {

			currentIndex = 6;
			cells[0].GetComponent<Animator>().SetBool("Empty", false);
			cells[1].GetComponent<Animator>().SetBool("Empty", false);
			cells[2].GetComponent<Animator>().SetBool("Empty", false);
			cells[3].GetComponent<Animator>().SetBool("Empty", false);
			cells[4].GetComponent<Animator>().SetBool("Empty", false);
			cells[5].GetComponent<Animator>().SetBool("Empty", false);
			cells[6].GetComponent<Animator>().SetBool("Empty", true);

			cells[0].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[1].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[2].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[3].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[4].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[5].GetComponent<Animator>().SetBool("CurrentAnimation", true);
			cells[6].GetComponent<Animator>().SetBool("CurrentAnimation", false);
		}
		else if (currentIndex != 5 && GameManager.powerInLevel <= 50 && GameManager.powerInLevel > 40) {

			currentIndex = 5;
			cells[0].GetComponent<Animator>().SetBool("Empty", false);
			cells[1].GetComponent<Animator>().SetBool("Empty", false);
			cells[2].GetComponent<Animator>().SetBool("Empty", false);
			cells[3].GetComponent<Animator>().SetBool("Empty", false);
			cells[4].GetComponent<Animator>().SetBool("Empty", false);
			cells[5].GetComponent<Animator>().SetBool("Empty", true);
			cells[6].GetComponent<Animator>().SetBool("Empty", true);

			cells[0].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[1].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[2].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[3].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[4].GetComponent<Animator>().SetBool("CurrentAnimation", true);
			cells[5].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[6].GetComponent<Animator>().SetBool("CurrentAnimation", false);
		}
		else if (currentIndex != 4 && GameManager.powerInLevel <= 40 && GameManager.powerInLevel > 30) {

			currentIndex = 4;
			cells[0].GetComponent<Animator>().SetBool("Empty", false);
			cells[1].GetComponent<Animator>().SetBool("Empty", false);
			cells[2].GetComponent<Animator>().SetBool("Empty", false);
			cells[3].GetComponent<Animator>().SetBool("Empty", false);
			cells[4].GetComponent<Animator>().SetBool("Empty", true);
			cells[5].GetComponent<Animator>().SetBool("Empty", true);
			cells[6].GetComponent<Animator>().SetBool("Empty", true);

			cells[0].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[1].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[2].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[3].GetComponent<Animator>().SetBool("CurrentAnimation", true);
			cells[4].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[5].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[6].GetComponent<Animator>().SetBool("CurrentAnimation", false);
		}
		else if (currentIndex != 3 && GameManager.powerInLevel <= 30 && GameManager.powerInLevel > 20) {
			currentIndex = 3;

			cells[0].GetComponent<Animator>().SetBool("Empty", false);
			cells[1].GetComponent<Animator>().SetBool("Empty", false);
			cells[2].GetComponent<Animator>().SetBool("Empty", false);
			cells[3].GetComponent<Animator>().SetBool("Empty", true);
			cells[4].GetComponent<Animator>().SetBool("Empty", true);
			cells[5].GetComponent<Animator>().SetBool("Empty", true);
			cells[6].GetComponent<Animator>().SetBool("Empty", true);

			cells[0].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[1].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[2].GetComponent<Animator>().SetBool("CurrentAnimation", true);
			cells[3].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[4].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[5].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[6].GetComponent<Animator>().SetBool("CurrentAnimation", false);

		}
		else if (currentIndex != 2 && GameManager.powerInLevel <= 20 && GameManager.powerInLevel > 10) {

			currentIndex = 2;

			cells[0].GetComponent<Animator>().SetBool("Empty", false);
			cells[1].GetComponent<Animator>().SetBool("Empty", false);
			cells[2].GetComponent<Animator>().SetBool("Empty", true);
			cells[3].GetComponent<Animator>().SetBool("Empty", true);
			cells[4].GetComponent<Animator>().SetBool("Empty", true);
			cells[5].GetComponent<Animator>().SetBool("Empty", true);
			cells[6].GetComponent<Animator>().SetBool("Empty", true);

			cells[0].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[1].GetComponent<Animator>().SetBool("CurrentAnimation", true);
			cells[2].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[3].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[4].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[5].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[6].GetComponent<Animator>().SetBool("CurrentAnimation", false);

		}
		else if (currentIndex != 1 && GameManager.powerInLevel <= 10 && GameManager.powerInLevel > 0) {

			currentIndex = 1;

			cells[0].GetComponent<Animator>().SetBool("Empty", false);
			cells[1].GetComponent<Animator>().SetBool("Empty", true);
			cells[2].GetComponent<Animator>().SetBool("Empty", true);
			cells[3].GetComponent<Animator>().SetBool("Empty", true);
			cells[4].GetComponent<Animator>().SetBool("Empty", true);
			cells[5].GetComponent<Animator>().SetBool("Empty", true);
			cells[6].GetComponent<Animator>().SetBool("Empty", true);

			cells[0].GetComponent<Animator>().SetBool("CurrentAnimation", true);
			cells[1].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[2].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[3].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[4].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[5].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[6].GetComponent<Animator>().SetBool("CurrentAnimation", false);

		}
		else if (currentIndex != 0 && GameManager.powerInLevel <= 0) {

			currentIndex = 0;
			cells[0].GetComponent<Animator>().SetBool("Empty", true);
			cells[1].GetComponent<Animator>().SetBool("Empty", true);
			cells[2].GetComponent<Animator>().SetBool("Empty", true);
			cells[3].GetComponent<Animator>().SetBool("Empty", true);
			cells[4].GetComponent<Animator>().SetBool("Empty", true);
			cells[5].GetComponent<Animator>().SetBool("Empty", true);
			cells[6].GetComponent<Animator>().SetBool("Empty", true);

			cells[0].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[1].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[2].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[3].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[4].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[5].GetComponent<Animator>().SetBool("CurrentAnimation", false);
			cells[6].GetComponent<Animator>().SetBool("CurrentAnimation", false);
		}
	}
}
