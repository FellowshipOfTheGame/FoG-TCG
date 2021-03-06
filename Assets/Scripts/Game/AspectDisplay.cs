﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectDisplay : MonoBehaviour {
    public bool reverse;
    Board board;

    [HideInInspector] public int index;

	// Use this for initialization
	void Start () {
        board = GameObject.FindObjectOfType<Board>() as Board;

	}
	
	// Update is called once per frame
	void Update () {
        int i;
        for (i = 0; i < 3; i++) {
            if (!reverse) {
                this.transform.GetChild(i).GetChild(0).GetComponent<TextMesh>().text = board.players[index].aspects[i].ToString();
                if (board.players[index].aspects[i] == 0)
                    this.transform.GetChild(i).gameObject.SetActive(false);
                else
                    this.transform.GetChild(i).gameObject.SetActive(true);
            }else {
                this.transform.GetChild(i).GetChild(0).GetComponent<TextMesh>().text = board.players[index].aspects[i].ToString();
                if (board.players[index].aspects[i] == 0)
                    this.transform.GetChild(i).gameObject.SetActive(false);
                else
                    this.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
	}
}
