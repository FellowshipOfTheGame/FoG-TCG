using UnityEngine;
using System.Collections;

public class Tabs : MonoBehaviour {

	public GameObject Tab1;
	public GameObject Tab2;
	public GameObject Tab3;
	public GameObject Tab4;

	void hideAll() {
		Tab1.SetActive (false);
		Tab2.SetActive (false);
		Tab3.SetActive (false);
		Tab4.SetActive (false);
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
}
