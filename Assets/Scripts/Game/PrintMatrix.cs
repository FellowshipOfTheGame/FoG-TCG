using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintMatrix : MonoBehaviour {

    Board board;

	// Use this for initialization
	void Start () {
        board = GameObject.FindObjectOfType<Board>() as Board;
	}
	
	// Update is called once per frame
	void Update () {
        string message = "Matrix\n";
        int i, j;
        for (i = 3; i >= 0; i--) {
            for (j = 0; j <= 4; j++) {
                if (board.cardMatrix[i, j] == null)
                    message += " \t";
                else
                    message += board.cardMatrix[i, j].GetComponent<Card>().type.ToString() + "\t";
            }
            message = message.Remove(message.Length - 1);
            message += "\n";
        }
        message = message.Remove(message.Length - 1);
        this.transform.GetChild(0).GetComponent<TextMesh>().text = message;
	}
}
