using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//[RequireComponent(typeof(AudioSource))]
public class VolumeControl : MonoBehaviour {

	public AudioSource Music;
	public AudioSource Effect1;

	public Slider Master;   // volume de tudo
	public Slider volumeM;  // volume de musica
	public Slider volumeE;  // de efeitos

	private float aux;
	private float vM;
	private float vE;

	void Start() {

		//PlayerPrefs.DeleteAll ();
		
		if (PlayerPrefs.HasKey ("MasterVolume")) {
			Master.value = PlayerPrefs.GetFloat("MasterVolume");
			aux = PlayerPrefs.GetFloat("MasterVolume");
			volumeM.value = PlayerPrefs.GetFloat("MusicVolume");
			volumeE.value = PlayerPrefs.GetFloat("EffectsVolume");

		} else {
			volumeM.value = 1;
			volumeE.value = 1;
			Master.value = 1;

			aux = 1;
		}

		VolumeFundo ();
		VolumeActions ();

	}

	public void SaveVolume(){
		PlayerPrefs.SetFloat("MasterVolume", Master.value);
		PlayerPrefs.SetFloat("MusicVolume", volumeM.value);
		PlayerPrefs.SetFloat("EffectsVolume", volumeE.value);

	}

	public void VolumeFundo() {
		vM = volumeM.value * Master.value;
		setVolumeFundo ();

	}

	public void VolumeActions() {
		vE = volumeE.value * Master.value;
		setVolumeActions ();

	}

	private void setVolumeFundo() {
		Music.volume = vM;

	}

	private void setVolumeActions() {
		Effect1.volume = vE;

	}

	public void Mute() {
		if (Master.value != 0) {
			aux = Master.value;
			Master.value = 0;
		} else 
			Master.value = aux;
	}
}