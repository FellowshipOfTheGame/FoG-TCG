using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDisplay : MonoBehaviour {
    public bool reverse;
    Board board;

    AspectDisplay aspects;
    TextMesh mana, life, player;

    int index;
    public Animator notificationBox;
    public TextMesh notificationText;
    public Color yellow, green, red, gray;

	// Use this for initialization
	void Start () {
        board = GameObject.FindObjectOfType<Board>() as Board;
        if (!reverse) index = 0;
        else index = 1;

        player = this.transform.Find("PlayerIndex").GetChild(0).GetComponent<TextMesh>();
        mana = this.transform.Find("Mana").GetChild(0).GetComponent<TextMesh>();
        life = this.transform.Find("Life").GetChild(0).GetComponent<TextMesh>();
        aspects = this.transform.Find("AspectPainel").GetComponent<AspectDisplay>();
        aspects.index = index;
	}
	
	// Update is called once per frame
	void Update () {
        player.text = "Player " + (index+1).ToString();
        mana.text = "Mana: " + board.players[index].mana.ToString();
        life.text = "Vida: " + board.players[index].HP.ToString();
    }

    public void Refresh(){
        notificationBox.SetBool("current", !reverse);
        notificationBox.SetTrigger("switch");
    }

    public void Switch(){
        index = 1 - index;
        board.players[index].display = this;
        aspects.index = index;
    }

    public void notify(string kind, int value, float delay){
        notificationBox.SetBool("current", !reverse);
        notificationText.text = "+" + value.ToString() + " " + kind;
        if (value >= 0){
            if (kind == "Life"){
                notificationBox.SetInteger("index", 1);
                notificationText.color = green;
            }else{
                notificationBox.SetInteger("index", 0);
                notificationText.color = yellow;
            }
            notificationText.text = "+" + value.ToString() + " " + kind + "!";
        }else{
            if (kind == "Life"){
                notificationBox.SetInteger("index", 3);
                notificationText.color = red;
            }else{
                notificationBox.SetInteger("index", 2);
                notificationText.color = gray;
            }
            notificationText.text = value.ToString() + " " + kind + "...";
        }
        CancelInvoke();
        Invoke("ShowNotification", delay);
    }

    void ShowNotification(){
        notificationBox.SetTrigger("show");
    }
}
