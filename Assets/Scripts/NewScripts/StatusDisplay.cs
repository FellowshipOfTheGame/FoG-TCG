using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDisplay : MonoBehaviour {

    Board board;

	// Use this for initialization
	void Start () {
        board = GameObject.FindObjectOfType<Board>() as Board;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Find("PlayerIndex").GetChild(0).GetComponent<TextMesh>().text = "Player " + board.currPlayer.ToString();
        this.transform.Find("Mana").GetChild(0).GetComponent<TextMesh>().text = "Mana: " + board.players[board.currPlayer - 1].mana.ToString();
        this.transform.Find("Life").GetChild(0).GetComponent<TextMesh>().text = "Life " + board.players[board.currPlayer - 1].HP.ToString();
        this.transform.Find("Time").GetChild(0).GetComponent<TextMesh>().text = "Time " + Mathf.Floor(board.limit - board.time).ToString();
    }
}
