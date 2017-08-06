using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardClick : Clickable {

    public Board board;
    public float elevation;
    Vector3 diff;
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
    }

    public override void OnClick(int mouseButton) {
        if (inHand) {
            if (mouseButton == 0 && CanBePlayed()) {
                isDragging = true;
                Slot.isChoosingPlace = true;
                originPos = new Vector3(this.transform.position.x, this.transform.position.y - elevation, this.transform.position.z); ;
                diff = this.transform.position - board.mousePosition;
                diff.z = 0.0f;
                this.GetComponent<BoxCollider>().enabled = false;
                board.dragCard = this.gameObject;
            }
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
            this.transform.parent.GetComponent<Player>().mana -= this.GetComponent<Card>().cost;
            this.transform.SetParent(board.slot.transform);
            board.players[board.currPlayer - 1].GetComponent<Player>().RefreshChildPositon();
            this.transform.position = board.slot.transform.position;
            inHand = false;
            //this.GetComponent<Card>().OnEnter();
        }else {
            this.transform.position = originPos;
        }
        Slot.isChoosingPlace = false;
        this.GetComponent<BoxCollider>().enabled = true;
        board.dragCard = null;
    }

    public override void OnPointerEnter() {
        if (inHand && CanBePlayed())
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + elevation, this.transform.position.z);
    }

    public override void OnPointerExit() {
        if (inHand && CanBePlayed())
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - elevation, this.transform.position.z);
    }

    public bool CanBePlayed() {
        int i;
        for (i = 0; i < 4; i++) {
            if (this.transform.parent.GetComponent<Player>().aspects[i] < this.GetComponent<Card>().aspects[i])
                return false;
        }
        if (this.transform.parent.GetComponent<Player>().mana >= this.GetComponent<Card>().cost)
            return true;
        else
            return false;
    }
}
