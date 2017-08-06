using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : Clickable {

    public static bool isChoosingPlace = false;
    new SpriteRenderer renderer;
    Color slotColor;
    Board board;
    public int[] pos;
    bool rightPlace;

	void Start() {
        board = GameObject.FindObjectOfType<Board>() as Board;
        renderer = GetComponent<SpriteRenderer> ();
        slotColor = renderer.color;
        renderer.color = Color.clear;

	}

    void Update() {
        
        if (isChoosingPlace) {
            char cardType = board.dragCard.GetComponent<Card>().type;
            if ((cardType != 'a' && board.currPlayer == pos[0]) || (cardType == 'a' && pos[0] == 3))
                rightPlace = true;
            else
                rightPlace = false;
        }
    }

	public override void OnPointerEnter() {
        if (isChoosingPlace && rightPlace) {
            renderer.color = slotColor;
            board.slot = this.gameObject;
        }
	}

	public override void OnPointerExit() {
        if (isChoosingPlace && rightPlace) {
            renderer.color = Color.clear;
            board.slot = null;
        }
	}
		
}
