using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour {

	public GameObject Config;
	public GameObject play;
	public GameObject pause;
	public GameObject Confirm;
    [Space(10)]
    public Raycaster raycaster;

	private bool paused = false;

	public void Confirmar() {
		Time.timeScale = 1;
		SceneManager.LoadScene ("Menu");
	}

	public void Ficar() {
		Confirm.SetActive (false);
	}

	public void sair () {
		Confirm.SetActive (true);
	}

	public void playPause() {
		if (paused) {
			Time.timeScale = 1;
			pause.SetActive (true);
			play.SetActive (false);
            raycaster.enabled = true;
			paused = false;
		} else {
			Time.timeScale = 0;
			pause.SetActive (false);
			play.SetActive (true);
            raycaster.enabled = false;
			paused = true;
		}
	}

	public void showConfig() {
		Config.SetActive (!Config.activeInHierarchy);
	}
}
