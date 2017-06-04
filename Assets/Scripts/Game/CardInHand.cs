using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardInHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {

    bool CanBePlayed = false;
    public bool inHand = false;
    GameObject clone;
    public GameObject miniCard;
    public GameObject illusionCard;
    public int cost;
    public int[] aspects;
    Vector3 diff;
    Vector3 OriginalPos;

    // Use this for initialization
    void Start() {
        cost = transform.GetComponent<AddCardInformation>().card.cost;
        aspects = transform.GetComponent<AddCardInformation>().card.aspects;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().canPlay && CanBePlayed && inHand)
            transform.position = new Vector3(transform.position.x, transform.parent.position.y + 10, transform.position.z);

        clone = Instantiate(illusionCard, transform.parent.parent) as GameObject;
        clone.GetComponent<AddCardInformation>().card = transform.GetComponent<AddCardInformation>().card;
        clone.transform.position = transform.position + new Vector3(0, 100, 0);
        clone.GetComponent<CanvasGroup>().blocksRaycasts = false;
        Board.hologram = clone;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (inHand) {
            Destroy(clone);
            clone = null;
            OriginalPos = new Vector3(transform.position.x, transform.parent.position.y, transform.position.z);
            Hand.dragCard = this.transform.gameObject;
            transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
            diff = transform.position - Input.mousePosition;
            Board.draggingCard = true;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (inHand)
            transform.position = Input.mousePosition + diff;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (inHand){
            if (Hand.chosenSlot != null && CanBePlayed) {
                Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().mana -= transform.GetComponent<CardInHand>().cost;
                GameObject newCard = Instantiate(miniCard,FindObjectOfType<Board>().transform) as GameObject;
                newCard.GetComponent<AddCardInformation>().card = this.transform.GetComponent<AddCardInformation>().card;
                newCard.transform.position = Hand.chosenSlot.transform.position;

               
                newCard.GetComponent<CardInTable>().onTable = true;
                if (this.GetComponent<AddCardInformation>().card.type == 'a') {
                    int i;
                    if (Hand.chosenSlot.transform.childCount > 0) {
                        int[] aspectsToRemove = Board.atm.GetComponent<CardInTable>().aspectsToGive;

                        for (i = 0; i < 4; i++) {
                            Board.player[0].GetComponent<PlayerStatus>().OwnAspects[i] -= aspectsToRemove[i];
                            Board.player[1].GetComponent<PlayerStatus>().OwnAspects[i] -= aspectsToRemove[i];
                        }
                        Destroy(Hand.chosenSlot.transform.GetChild(0).gameObject);
                    }
                    Board.atm = newCard;
                    newCard.transform.SetParent(Hand.chosenSlot.transform);
                    int[] aspectsToAdd = transform.GetComponent<CardInTable>().aspectsToGive;

                    for (i = 0; i < 4; i++) {
                        Board.player[0].GetComponent<PlayerStatus>().OwnAspects[i] += aspectsToAdd[i];
                        Board.player[1].GetComponent<PlayerStatus>().OwnAspects[i] += aspectsToAdd[i];
                    }

                } else if (this.GetComponent<AddCardInformation>().card.type == 't') {
                    int i;

                    newCard.GetComponent<CardInTable>().canFarm = true;
                    newCard.GetComponent<Image>().enabled = false;
                    for (i = 0; i < newCard.transform.childCount; i++) {
                        newCard.transform.GetChild(i).gameObject.SetActive(false);
                    }

                    if (Board.currPlayer == 1)
                        Board.cardMatriz[0, Hand.chosenSlot.GetComponent<Slot>().pos[1]] = newCard;
                    else
                        Board.cardMatriz[3, Hand.chosenSlot.GetComponent<Slot>().pos[1]] = newCard;

                    

                    int[] aspectsToAdd = GetComponent<CardInTable>().aspectsToGive;
                    int[] PlayerAspectsList = Board.player[Board.currPlayer-1].GetComponent<PlayerStatus>().OwnAspects;

                    for (i = 0; i < 4; i++) {
                            PlayerAspectsList[i]+=aspectsToAdd[i];
                    }
                } else if (this.GetComponent<AddCardInformation>().card.type == 'c') {
                    newCard.transform.SetParent(Hand.chosenSlot.transform);
                    Board.cardMatriz[Hand.chosenSlot.GetComponent<Slot>().pos[0], Hand.chosenSlot.GetComponent<Slot>().pos[1]] = newCard;
                    Hand.chosenSlot.GetComponent<Slot>().IsFull = true;
                    newCard.GetComponent<CardInTable>().canAttack = true;
                }

                Hand.chosenSlot = null;
                Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().canMove = false;
                Destroy(transform.gameObject);
            } else {
                transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
                transform.position = OriginalPos;
            }
            Board.draggingCard = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (inHand) {
            transform.position = new Vector3(transform.position.x, transform.parent.position.y, transform.position.z);
            if (clone != null) {
                Destroy(clone);
                clone = null;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (inHand && Board.currPlayer==transform.parent.parent.GetComponent<PlayerStatus>().playerIndex) {
            if (cost <= transform.parent.parent.GetComponent<PlayerStatus>().mana) {
                CanBePlayed = true;
                transform.Find("Cost").GetComponent<Text>().color = Color.white;
                int i, j, aux = 0;
                for (i = 0; i < 4; i++) {
                    for (j = 1; j <= aspects[i]; j++) {
                        if (transform.parent.parent.GetComponent<PlayerStatus>().OwnAspects[i] >= j)
                            transform.Find("Aspects").GetChild(aux + j - 1).GetComponent<Image>().color = Color.white;
                        else
                            transform.Find("Aspects").GetChild(aux + j - 1).GetComponent<Image>().color = Color.black;
                    }
                    aux += aspects[i];
                    if (transform.parent.parent.GetComponent<PlayerStatus>().OwnAspects[i] < aspects[i])
                        CanBePlayed = false;
                }
                  

            } else {
                CanBePlayed = false;
                transform.Find("Cost").GetComponent<Text>().color = Color.red;

            }

        }
    }

}
