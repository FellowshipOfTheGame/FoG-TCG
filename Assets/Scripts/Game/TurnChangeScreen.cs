using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnChangeScreen : MonoBehaviour {

	public Text title;
    public GameObject screen;

    Animator anim;
	
    // Use this for initialization
    void Start () {
        anim = this.GetComponent<Animator>();
        //screen.SetActive(false);
    }

	public void show(int player){
        //screen.SetActive(true);
        //title.text = GameManager.instance.names[player - 1];
        title.text = "Player " + player.ToString();
        anim.SetTrigger("show");
    }

	public void hide(){
        //screen.SetActive(false);
        anim.SetTrigger("hide");
    }

    public void reset(){
        anim = this.GetComponent<Animator>();
        title.text = "Player 1";
        anim.SetTrigger("start");
    }
}