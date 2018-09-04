using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyCard : Clickable {

    Board board;

    public Sprite[] sprites;

    SpriteRenderer spr;
    // Use this for initialization
    void Start () {
        board = this.transform.parent.parent.GetComponent<Board>();
        spr = this.GetComponent<SpriteRenderer>();
        spr.sprite = sprites[0];
    }

    public override void OnPointerEnter(){
        if (board.players[board.currPlayer - 1].canBuy)
            spr.sprite = sprites[1];
    }
    public override void OnClick(int mouseButton) {
        if (board.players[board.currPlayer - 1].GetComponent<Player>().canBuy) {
            board.players[board.currPlayer - 1].GetComponent<Player>().PickUpCard();
            board.players[board.currPlayer - 1].GetComponent<Player>().canBuy = false;
            spr.sprite = sprites[0];
        }
    }

    public override void OnPointerExit(){
        spr.sprite = sprites[0];
    }
}
