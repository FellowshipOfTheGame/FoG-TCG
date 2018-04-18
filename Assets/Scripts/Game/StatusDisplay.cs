using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDisplay : MonoBehaviour {
    public bool reverse;
    Board board;

	// Use this for initialization
	void Start () {
        board = GameObject.FindObjectOfType<Board>() as Board;
	}
	
	// Update is called once per frame
	void Update () {
        if (!reverse) {
            this.transform.Find("PlayerIndex").GetChild(0).GetComponent<TextMesh>().text = "Player " + board.currPlayer.ToString();
            this.transform.Find("Mana").GetChild(0).GetComponent<TextMesh>().text = "Mana: " + board.players[board.currPlayer - 1].mana.ToString();
            this.transform.Find("Life").GetChild(0).GetComponent<TextMesh>().text = "Life " + board.players[board.currPlayer - 1].HP.ToString();
            float aux = Mathf.Ceil(board.limit - board.time);

            if (aux >= 10)
                this.transform.Find("Time").GetChild(0).GetComponent<TextMesh>().text = aux.ToString();
            else
                this.transform.Find("Time").GetChild(0).GetComponent<TextMesh>().text = "0" + aux.ToString();
  
        }else {
            this.transform.Find("PlayerIndex").GetChild(0).GetComponent<TextMesh>().text = "Player " + (3 - board.currPlayer).ToString();
            this.transform.Find("Mana").GetChild(0).GetComponent<TextMesh>().text = "Mana: " + board.players[2 - board.currPlayer].mana.ToString();
            this.transform.Find("Life").GetChild(0).GetComponent<TextMesh>().text = "Life " + board.players[2 - board.currPlayer].HP.ToString();
            this.transform.Find("Time").GetChild(0).GetComponent<TextMesh>().text = " ";
        }
    }
}
