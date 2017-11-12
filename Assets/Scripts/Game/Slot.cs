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
    public GameObject[] cards = new GameObject[2]; //card[0]=terrain/atmosphere; card[1]=creature
    bool rightPlace;
    public bool isFull;
    public GameObject genericIllusion;

	void Start() {
        board = GameObject.FindObjectOfType<Board>() as Board;
        renderer = GetComponent<SpriteRenderer> ();
        slotColor = renderer.color;
        renderer.color = Color.clear;
        cards[0] = null;
        cards[1] = null;
	}

    void Update() {
        
        if (isChoosingPlace) {
            char cardType = board.dragCard.GetComponent<Card>().type;
            if ((cardType != 'a' && board.currPlayer == pos[0]) || (cardType == 'a' && pos[0] == 4))
                rightPlace = true;
            else
                rightPlace = false;

            //checking if slot don't have a card with a same type
            
            if (cardType == 'c' && cards[1] == null) {
                isFull = false;
            } else if ((cardType == 't') && cards[0] == null) {
                isFull = false;
            } else if (cardType == 's' || cardType == 'a') {
                isFull = false;
            } else {
                isFull = true;
            }

        }
    }

	public override void OnPointerEnter() {
       
        if (isChoosingPlace && rightPlace && !isFull) {
            renderer.color = slotColor;
            board.slot = this.gameObject;
        }

        if (this.transform.childCount != 0 && !isChoosingPlace)
            MakeIllusion();
	}

	public override void OnPointerExit() {
        if (board.illusionPos.childCount != 0)
            Destroy(board.illusionPos.GetChild(0).gameObject);

        if (board.illusionPos2.childCount != 0)
            Destroy(board.illusionPos2.GetChild(0).gameObject);

        if (isChoosingPlace && rightPlace && !isFull) {
            renderer.color = Color.clear;
            board.slot = null;
        }
	}
		
    void MakeIllusion() {
        if (board.illusionPos.childCount != 0)
            Destroy(board.illusionPos.GetChild(0).gameObject);

        if (cards[1] != null) {
            GameObject illusion = Instantiate(genericIllusion, board.illusionPos) as GameObject;
            illusion.GetComponent<IllusionScript>().original = cards[1];
            illusion.transform.position = board.illusionPos.position;
        }

        if (cards[0] != null) {
            GameObject illusion = Instantiate(genericIllusion, board.illusionPos2) as GameObject;
            illusion.GetComponent<IllusionScript>().original = cards[0];
            illusion.transform.position = board.illusionPos2.position;
        }
    }
}
