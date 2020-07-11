using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : Clickable {

    Board board;
    bool working = false;

    public Sprite[] sprites;

    SpriteRenderer spr;
    // Use this for initialization
    void Awake () {
        board = GameObject.FindObjectOfType<Board>() as Board;
        spr = this.GetComponent<SpriteRenderer>();
        spr.sprite = sprites[0];
    }
	
	// Update is called once per frame
	void Update () {

	}

    public override void OnPointerEnter(){
        if (working)
            spr.sprite = sprites[1];
    }

    public override void OnClick(int mouseButton) {
        if (mouseButton == 0 && working){
            spr.sprite = sprites[0];
            board.EndTurn();
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
