using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnChangeScreen : MonoBehaviour {

	public Text title;
    public GameObject screen;
	
    // Use this for initialization
    void Start () {
        screen.SetActive(false);
    }

	public void show(int player){
        screen.SetActive(true);
        title.text = "Player " + player.ToString();
    }

	public void hide(){
        screen.SetActive(false);
    }
}