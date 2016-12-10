using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//[RequireComponent(typeof(AudioSource))]
public class PlaySceneMusic : MonoBehaviour {
	
	public AudioSource Music;
	
	private int i;
	private int aux;

	void Start() {
		if (PlayerPrefs.HasKey ("MasterVolume"))
			Music.volume = PlayerPrefs.GetFloat ("MasterVolume") * PlayerPrefs.GetFloat ("MusicVolume");
		else Music.volume = 1;
	}
		
	public IEnumerator FadeOut(){
		aux = (int)(Music.volume * 10);
		
		for (i = aux; i > 0; i--) {
			Music.volume = i * 0.1f;
			
			yield return new WaitForSeconds(0.15f);
		}

	}
}