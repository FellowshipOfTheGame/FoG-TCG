using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardClick : Clickable {

    public Board board;
    public float elevation;
    public GameObject genericIllusion;
    Vector3 diff;
    public bool isDragging = false;
    bool inHand = true;
    public Vector3 originPos;
    Vector3 colliderSize;

    // Use this for initialization
    void Start () {
        board = GameObject.FindObjectOfType<Board>() as Board;
        colliderSize = this.GetComponent<BoxCollider>().size;
    }
	
	// Update is called once per frame
	void Update () {
        if (isDragging) {
            OnDragging();
            if (Input.GetMouseButtonUp(0))
                OnDropping();
        }

        if (Slot.isChoosingPlace)
            this.GetComponent<BoxCollider>().size = new Vector3(0.0f, 0.0f, 0.0f);
        else
            this.GetComponent<BoxCollider>().size = colliderSize;
    }

    public override void OnClick(int mouseButton) {
        if (inHand)
        {
            if (mouseButton == 0 && CanBePlayed())
            {
                isDragging = true;
                Slot.isChoosingPlace = true;
                originPos = new Vector3(this.transform.position.x, this.transform.parent.position.y, this.transform.position.z); ;
                diff = this.transform.position - board.mousePosition;
                diff.z = 0.0f;
                this.GetComponent<BoxCollider>().enabled = false;
                board.dragCard = this.gameObject;
            }
        }
        else
        {
            GetComponent<Card>().Attack();
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
            
            int[] pos = board.slot.GetComponent<Slot>().pos;
            board.slot.GetComponent<SpriteRenderer>().color = Color.clear;

            if (this.GetComponent<Card>().type == 'c') {
                board.slot.GetComponent<Slot>().cards[1] = this.gameObject;
                board.cardMatrix[pos[0], pos[1]] = this.gameObject;
                this.GetComponent<Card>().pos = pos;
            } else if (this.GetComponent<Card>().type == 't') {
                board.slot.GetComponent<Slot>().cards[0] = this.gameObject;
                //make card terrain invisible and unclickable
                this.GetComponent<SpriteRenderer>().enabled = false;
                int i;
                for (i = 0; i < this.transform.childCount; i++)
                    this.transform.GetChild(i).gameObject.SetActive(false);

                this.GetComponent<BoxCollider>().size = new Vector3(0.0f, 0.0f, 0.0f);
                colliderSize = new Vector3(0.0f, 0.0f, 0.0f);

                if (board.currPlayer == 1) {
                    board.cardMatrix[0, pos[1]] = this.gameObject;
                    this.GetComponent<Card>().pos[0] = 0;
                    this.GetComponent<Card>().pos[1] = pos[1];
                } else {
                    board.cardMatrix[3, pos[1]] = this.gameObject;
                    this.GetComponent<Card>().pos[0] = 3;
                    this.GetComponent<Card>().pos[1] = pos[1];
                }
            } else if (this.GetComponent<Card>().type == 'a') {
                board.slot.GetComponent<Slot>().cards[0] = this.gameObject;
                this.GetComponent<Card>().pos[0] = -1;
                this.GetComponent<Card>().pos[1] = -1;
                board.cardAtm = this.gameObject;
            }
            board.players[board.currPlayer - 1].capt.canMove = false;
            board.slot = null;
            this.GetComponent<Card>().OnEnter();
        } else {
            this.transform.position = originPos;
        }
        Slot.isChoosingPlace = false;
        this.GetComponent<BoxCollider>().enabled = true;
        board.dragCard = null;
    }

    public override void OnPointerEnter() {
        MakeIllusion();

        if (inHand && CanBePlayed())
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + elevation, this.transform.position.z);
    }

    public override void OnPointerExit() {
        if (board.illusionPos.childCount != 0)
            Destroy(board.illusionPos.GetChild(0).gameObject);

        if (board.illusionPos2.childCount != 0)
            Destroy(board.illusionPos2.GetChild(0).gameObject);

        if (inHand && CanBePlayed())
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - elevation, this.transform.position.z);
    }

    public bool CanBePlayed() {
        int i;
        if (!board.players[board.currPlayer - 1].canPlay)
            return false;

        for (i = 0; i < 4; i++) {
            if (this.transform.parent.GetComponent<Player>().aspects[i] < this.GetComponent<Card>().aspects[i])
                return false;
        }
        if (this.transform.parent.GetComponent<Player>().mana >= this.GetComponent<Card>().cost)
            return true;
        else
            return false;
    }

    void MakeIllusion() {
        if (board.illusionPos.childCount != 0)
            Destroy(board.illusionPos.GetChild(0).gameObject);

        GameObject illusion = Instantiate(genericIllusion, board.illusionPos) as GameObject;
        illusion.GetComponent<IllusionScript>().original = this.gameObject;
        illusion.transform.position = board.illusionPos.position;

        if (!inHand) {
            if (board.illusionPos2.childCount != 0)
                Destroy(board.illusionPos2.GetChild(0).gameObject);

            if(this.transform.parent.GetComponent<Slot>().cards[0] != null) {
                illusion = Instantiate(genericIllusion, board.illusionPos2) as GameObject;
                illusion.GetComponent<IllusionScript>().original = this.transform.parent.GetComponent<Slot>().cards[0];
                illusion.transform.position = board.illusionPos2.position;
            }
        }
    }
}
