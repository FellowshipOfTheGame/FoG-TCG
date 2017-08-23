using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : Clickable {

    Board board;

	// Use this for initialization
	void Start () {
        board = GameObject.FindObjectOfType<Board>() as Board;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnClick(int mouseButton) {
        if (mouseButton == 0)
            board.EndTurn();
    }
}
