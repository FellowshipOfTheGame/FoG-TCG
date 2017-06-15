using UnityEngine;
using System.Collections;

public class Tabs : MonoBehaviour {

	public GameObject Tab1;
	public GameObject Tab2;
	public GameObject Tab3;
	public GameObject Tab4;
	public GameObject Tab5;
	public GameObject Tab6;

	void hideAll() {
		Tab1.SetActive (false);
		Tab2.SetActive (false);
		Tab3.SetActive (false);
		Tab4.SetActive (false);
		Tab5.SetActive (false);
		Tab6.SetActive (false);
	}

	public void ShowTab1() {
		hideAll ();
		Tab1.SetActive (true);
	}

	public void ShowTab2() {
		hideAll ();
		Tab2.SetActive (true);
	}

	public void ShowTab3() {
		hideAll ();
		Tab3.SetActive (true);
	}

	public void ShowTab4() {
		hideAll ();
		Tab4.SetActive (true);
	}

	public void ShowTab5() {
		hideAll ();
		Tab5.SetActive (true);
	}

	public void ShowTab6() {
		hideAll ();
		Tab6.SetActive (true);
	}
}
