using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    //comprar cartas
    Deck playerDeck;
    public GameObject genericCard;

    //jogar cartas no campo
    Vector3 diff;
    public GameObject chosenCard = null;
    public static GameObject dragCard = null;
    public static GameObject chosenSlot = null;

	// Use this for initialization
	void Start () {
        playerDeck = transform.parent.GetComponent<Deck>();
    }

    // Update is called once per frame
    void Update () {
        if (dragCard != null) {
            DragCard();

            if (Input.GetMouseButtonUp(0))
                DragCardEnd();
        }

        if (playerDeck.deckList.Count == 0)
            transform.parent.GetComponent<PlayerStatus>().canBuy = false;
	}

    //clicando na carta
    public void DragCardStart(GameObject card) {
        chosenCard = card;
        dragCard = Instantiate(card, transform.parent) as GameObject;
        dragCard.GetComponent<CardInHand>().enabled = false;
        diff = dragCard.transform.position - Input.mousePosition;
        Slot.choosingPlace = true;
    }

    //arrastando a carta
    void DragCard() {
        dragCard.transform.position = Input.mousePosition + diff;
    }

    void DragCardEnd() {
        if (chosenSlot != null) {
            //girar a carta
            dragCard.transform.position = chosenSlot.transform.position;
            if(Board.currPlayer==1)
                dragCard.transform.RotateAround(dragCard.transform.position, Vector3.forward, 270);
            else
                dragCard.transform.RotateAround(dragCard.transform.position, Vector3.forward, 90);
       
            //posicionar e registrar
            dragCard.transform.SetParent(chosenSlot.transform);
            Board.cardMatriz[chosenSlot.GetComponent<Slot>().pos[0], chosenSlot.GetComponent<Slot>().pos[1]] = dragCard;
            if (dragCard.GetComponent<AddCardInformation>().card.type == 'c') {
                dragCard.GetComponent<CardAttack>().onTable = true;
                dragCard.GetComponent<CardAttack>().canAttack = true;
            }
            if (dragCard.GetComponent<AddCardInformation>().card.type == 't') {
                int i;
                ArrayList aspectsToAdd = dragCard.GetComponent<CardResourceGenerator>().aspectsToGive;
                ArrayList PlayerAspectsList = transform.parent.GetComponent<PlayerStatus>().OwnAspects;

                for (i=0;i< aspectsToAdd.Count; i++) {
                    if(!PlayerAspectsList.Contains(aspectsToAdd[i]))
                        PlayerAspectsList.Add(aspectsToAdd[i]);
                }
            }

            Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().mana -= dragCard.GetComponent<CardInHand>().cost;
            dragCard = null;
            //setar slot
            chosenSlot.GetComponent<Slot>().IsFull = true;
            chosenSlot = null;
            //finalizar
            Destroy(chosenCard);
            chosenCard = null;
            Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().canMove = false;
        } else {
            Destroy(dragCard);
            dragCard = null;
            chosenSlot = null;
        }
        Slot.choosingPlace = false;
    }

    //pegar uma carta aleatoria do deck
    public void PickUpCard() {
        if (transform.childCount < 7) {
            //escolhe uma carta; põe na mão; tira do deck
            int num = Random.Range(0, playerDeck.deckList.Count - 1);
            GameObject newCard = Instantiate(genericCard, transform);
            newCard.GetComponent<AddCardInformation>().card = playerDeck.deckList[num];
            playerDeck.deckList.RemoveAt(num);
            newCard = null;
        }
    }
}
