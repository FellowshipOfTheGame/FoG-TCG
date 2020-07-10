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
    public GameObject gate;
    bool moveGate = false;
    float gatepos, destiny;
    public GameObject genericIllusion;

	void Start() {
        if(gate != null)
            gatepos = gate.transform.position.y;
        board = GameObject.FindObjectOfType<Board>() as Board;
        if (pos[0] < 4)
            board.slotMatrix[pos[0], pos[1]] = this.gameObject;
        renderer = GetComponent<SpriteRenderer> ();
        slotColor = renderer.color;
        renderer.color = Color.clear;
        cards[0] = null;
        cards[1] = null;
	}

    void Update() {
        if (isChoosingPlace) {
            char cardType = board.dragCardType;
            if ((cardType == 's' && pos[0] != 4) || (cardType != 'a' && board.currPlayer == pos[0]) || (cardType == 'a' && pos[0] == 4))
                rightPlace = true;
            else
                rightPlace = false;

            //checking if slot don't have a card with a same type
            
            if (cardType == 'c' && cards[1] == null) {
                isFull = false;
            } else if ((cardType == 't') && cards[0] == null) {
                isFull = false;
            } else if (cardType == 's' || cardType == 'a' || cardType == 'x') {
                isFull = false;
            } else {
                isFull = true;
            }
        }

        if (moveGate) {
            if (gate.transform.position.y != destiny)
                gate.transform.position = Vector3.Lerp(gate.transform.position, new Vector3(gate.transform.position.x, destiny, gate.transform.position.z), 0.3f);
            else
                moveGate = false;
        }
    }

	public override void OnPointerEnter() {
        if ((isChoosingPlace && rightPlace && !isFull) || board.castCard != null) {
            renderer.color = slotColor;
            board.slot = this.gameObject;
        }

        if (this.transform.childCount != 0 && !isChoosingPlace)
            MakeIllusion();
	}

    public void show() {
        renderer.color = slotColor;
        board.slot = this.gameObject;
    }

    public void hide() {
        renderer.color = Color.clear;
        board.slot = null;
    }

    public override void OnPointerExit() {
        if (board.illusionPos.childCount != 0)
            Destroy(board.illusionPos.GetChild(0).gameObject);

        if (board.illusionPos2.childCount != 0)
            Destroy(board.illusionPos2.GetChild(0).gameObject);

        if ((isChoosingPlace && rightPlace && !isFull) || board.castCard != null) {
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

    public void Reset() {
        gate.transform.position = new Vector3(gate.transform.position.x, gatepos, gate.transform.position.z);
        cards[0] = null;
        cards[1] = null;
        for (int i = 0; i < this.transform.childCount; i++)
            Destroy(this.transform.GetChild(i).gameObject);
    }

    public void openGate() {
        moveGate = true;
        if (pos[0] <= 1)
            destiny = gatepos - 1.5f;
        else
            destiny = gatepos + 1.5f;
    }

    public void closeGate() {
        moveGate = true;
        destiny = gatepos;
    }
}
