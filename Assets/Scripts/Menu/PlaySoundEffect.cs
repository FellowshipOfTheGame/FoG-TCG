using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//[RequireComponent(typeof(AudioSource))]
public class PlaySoundEffect : MonoBehaviour {
	
	public AudioSource SoundEffect;

	void Start() {
		if (PlayerPrefs.HasKey ("MasterVolume"))
			SoundEffect.volume = PlayerPrefs.GetFloat ("MasterVolume") * PlayerPrefs.GetFloat ("EffectsVolume");
		else SoundEffect.volume = 1;
	}

	public void PlayEffect() {
		SoundEffect.Play ();
	}
}