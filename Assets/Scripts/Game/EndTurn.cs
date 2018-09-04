using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : Clickable {

    Board board;

    public Sprite[] sprites;

    SpriteRenderer spr;
    // Use this for initialization
    void Start () {
        board = GameObject.FindObjectOfType<Board>() as Board;
        spr = this.GetComponent<SpriteRenderer>();
        spr.sprite = sprites[0];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnPointerEnter(){
        spr.sprite = sprites[1];
    }

    public override void OnClick(int mouseButton) {
        if (mouseButton == 0){
            spr.sprite = sprites[0];
            board.EndTurn();
        }
    }

    public override void OnPointerExit(){
        spr.sprite = sprites[0];
    }
}
