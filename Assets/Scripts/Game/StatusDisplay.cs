using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDisplay : MonoBehaviour {
    public bool reverse;
    Board board;

    public Animator notificationBox;
    public TextMesh notificationText;
    public Color yellow, green, red, gray;

	// Use this for initialization
	void Start () {
        board = GameObject.FindObjectOfType<Board>() as Board;
	}
	
	// Update is called once per frame
	void Update () {
        if (!reverse) {
            //this.transform.Find("PlayerIndex").GetChild(0).GetComponent<TextMesh>().text = GameManager.instance.names[0];
            this.transform.Find("Mana").GetChild(0).GetComponent<TextMesh>().text = "Mana: " + board.players[0].mana.ToString();
            this.transform.Find("Life").GetChild(0).GetComponent<TextMesh>().text = "Life " + board.players[0].HP.ToString();
  
        }else {
            //this.transform.Find("PlayerIndex").GetChild(0).GetComponent<TextMesh>().text = GameManager.instance.names[1];
            this.transform.Find("Mana").GetChild(0).GetComponent<TextMesh>().text = "Mana: " + board.players[1].mana.ToString();
            this.transform.Find("Life").GetChild(0).GetComponent<TextMesh>().text = "Life " + board.players[1].HP.ToString();
        }
        notificationBox.SetBool("myTurn", (!reverse && board.currPlayer == 1) || (reverse && board.currPlayer == 2));
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
