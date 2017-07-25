using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyCard : MonoBehaviour, IPointerClickHandler {

    Board board;

	// Use this for initialization
	void Start () {
        board = this.transform.parent.parent.GetComponent<Board>();
	}

    public void OnPointerClick(PointerEventData eventData) {
        board.players[board.currPlayer - 1].GetComponent<Player>().PickUpCard();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
