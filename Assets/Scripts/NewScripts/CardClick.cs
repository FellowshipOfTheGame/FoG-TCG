using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardClick : Clickable {

    public Board board;
    Vector3 diff;
    bool mouseOver = false;
    bool isDragging = false;
    bool inHand = true;
    Vector3 originPos;

    // Use this for initialization
    void Start () {
        board = GameObject.FindObjectOfType<Board>() as Board;
    }
	
	// Update is called once per frame
	void Update () {
        if (isDragging) {
            OnDragging();
            if (Input.GetMouseButtonUp(0))
                OnDropping();
            
        }
        
        if (mouseOver && Input.GetMouseButtonDown(0))
            OnPointerClick();
        
    }

    public void OnPointerClick() {
        isDragging = true;
        if (inHand) {
            Slot.isChoosingPlace = true;
            originPos = this.transform.position;
            diff = this.transform.position - board.mousePosition;
            diff.z = 0.0f;
            this.GetComponent<BoxCollider>().enabled = false;
            board.dragCard = this.gameObject;
        }
    }


    public void OnDragging() {
        if (inHand) {
            this.transform.position = new Vector3(board.mousePosition.x, board.mousePosition.y, -3.55f) + diff;
        }
    }

    public void OnDropping() {
        isDragging = false;
        if (board.slot != null) {
            this.transform.SetParent(board.slot.transform);
            board.players[board.currPlayer - 1].GetComponent<Player>().RefreshChildPositon();
            this.transform.position = board.slot.transform.position;
            //this.GetComponent<Card>().OnEnter();
        }else {
            this.transform.position = originPos;
        }
        Slot.isChoosingPlace = false;
        this.GetComponent<BoxCollider>().enabled = true;
        board.dragCard = null;
    }

    public override void OnPointerEnter() {
        mouseOver = true;
    }

    public override void OnPointerExit() {
        mouseOver = false;
    }
}
