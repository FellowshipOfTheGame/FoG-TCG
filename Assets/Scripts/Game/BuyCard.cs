using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyCard : Clickable {

    Board board;

    public Sprite[] sprites;

    bool working = false;

    SpriteRenderer spr;
    // Use this for initialization
    void Awake () {
        board = this.transform.parent.parent.GetComponent<Board>();
        spr = this.GetComponent<SpriteRenderer>();
        spr.sprite = sprites[0];
    }

    public override void OnPointerEnter(){
        if (board.players[board.currPlayer - 1].capt.canBuy && working)
            spr.sprite = sprites[1];
    }
    public override void OnClick(int mouseButton) {
        if (board.players[board.currPlayer - 1].capt.canBuy && working) {
            board.players[board.currPlayer - 1].capt.Command();
            spr.sprite = sprites[0];
        }
    }

    public override void OnPointerExit(){
        if (working)
            spr.sprite = sprites[0];
    }

    public void SetWork(bool value){
        if (value) spr.sprite = sprites[0];
        else spr.sprite = sprites[2];

        working = value;
    }
}
