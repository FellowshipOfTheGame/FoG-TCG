using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyCard : Clickable {

    Board board;

    // Use this for initialization
    void Start () {
        board = this.transform.parent.parent.GetComponent<Board>();
	}

    public override void OnClick(int mouseButton) {
        board.players[board.currPlayer - 1].GetComponent<Player>().PickUpCard();
    }
}
