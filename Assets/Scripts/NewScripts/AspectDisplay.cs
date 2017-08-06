﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectDisplay : MonoBehaviour {

    Board board;

	// Use this for initialization
	void Start () {
        board = GameObject.FindObjectOfType<Board>() as Board;
	}
	
	// Update is called once per frame
	void Update () {
        int i;
        for (i = 0; i < 4; i++) {
            this.transform.GetChild(i).GetChild(0).GetComponent<TextMesh>().text= board.players[board.currPlayer - 1].GetComponent<Player>().aspects[i].ToString();
            if (board.players[board.currPlayer - 1].GetComponent<Player>().aspects[i] == 0)
                this.transform.GetChild(i).gameObject.SetActive(false);
            else
                this.transform.GetChild(i).gameObject.SetActive(true);
        }
	}
}
