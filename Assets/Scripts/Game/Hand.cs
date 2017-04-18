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

        if (card.GetComponent<AddCardInformation>().card.type == 'a')
            AtmButton.open = true;
    }

    //arrastando a carta
    void DragCard() {
        dragCard.transform.position = Input.mousePosition + diff;
    }

    void DragCardEnd() {
        if (chosenSlot != null) {
            Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().mana -= dragCard.GetComponent<CardInHand>().cost;
            dragCard.transform.position = chosenSlot.transform.position;

            if (dragCard.GetComponent<AddCardInformation>().card.type != 'a') {
                dragCard.transform.SetParent(chosenSlot.transform);
                //girar a carta
                if (Board.currPlayer == 1)
                    dragCard.transform.RotateAround(dragCard.transform.position, Vector3.forward, 270);
                else
                    dragCard.transform.RotateAround(dragCard.transform.position, Vector3.forward, 90);

                Board.cardMatriz[chosenSlot.GetComponent<Slot>().pos[0], chosenSlot.GetComponent<Slot>().pos[1]] = dragCard;
                //criatura
                if (dragCard.GetComponent<AddCardInformation>().card.type == 'c') {
                    dragCard.GetComponent<CardAttack>().onTable = true;
                    dragCard.GetComponent<CardAttack>().canAttack = true;
                }
                //terreno
                if (dragCard.GetComponent<AddCardInformation>().card.type == 't') {
                    dragCard.GetComponent<CardResourceGenerator>().canFarm = true;
                    int i;
                    ArrayList aspectsToAdd = dragCard.GetComponent<CardResourceGenerator>().aspectsToGive;
                    ArrayList PlayerAspectsList = transform.parent.GetComponent<PlayerStatus>().OwnAspects;

                    for (i = 0; i < aspectsToAdd.Count; i++) {
                        if (!PlayerAspectsList.Contains(aspectsToAdd[i]))
                            PlayerAspectsList.Add(aspectsToAdd[i]);
                    }
                }

            }else {
                int i;
                if (chosenSlot.GetComponent<Slot>().IsFull) {
                    ArrayList aspectsToRemove = chosenSlot.transform.GetChild(0).GetComponent<CardResourceGenerator>().aspectsToGive;
                   
                    for (i = 0; i < aspectsToRemove.Count; i++) {
                        if (Board.AtmAspects.Contains(aspectsToRemove[i]))
                            Board.AtmAspects.Remove(aspectsToRemove[i]);
                    }
                    Destroy(chosenSlot.transform.GetChild(0).gameObject);
                    chosenSlot.GetComponent<Slot>().IsFull=false;
                }

                dragCard.transform.SetParent(chosenSlot.transform);
                Board.atm = dragCard;
                ArrayList aspectsToAdd = dragCard.GetComponent<CardResourceGenerator>().aspectsToGive;

                for (i = 0; i < aspectsToAdd.Count; i++) {
                    if (!Board.AtmAspects.Contains(aspectsToAdd[i]))
                        Board.AtmAspects.Add(aspectsToAdd[i]);
                }
            }

            chosenSlot.GetComponent<Slot>().IsFull = true;
            dragCard = null;
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
        AtmButton.open = false;
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
