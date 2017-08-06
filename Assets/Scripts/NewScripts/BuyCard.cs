using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyCard : Clickable {

    Board board;
    bool mouseOver = false;

    // Use this for initialization
    void Start () {
        board = this.transform.parent.parent.GetComponent<Board>();
	}

    // Update is called once per frame
    void Update() {
        if (mouseOver && Input.GetMouseButtonDown(0))
            OnPointerClick();
    }

    public void OnPointerClick() {
        board.players[board.currPlayer - 1].GetComponent<Player>().PickUpCard();
    }
	
    public override void OnPointerEnter() {
        mouseOver = true;
    }

    public override void OnPointerExit() {
        mouseOver = false;
    }
}
