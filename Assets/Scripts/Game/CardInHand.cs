﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MoonSharp.Interpreter;

public class CardInHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {

    bool CanBePlayed = false;
    public bool inHand = false;
    GameObject clone;
	Card card;
    public GameObject miniCard;
    public GameObject illusionCard;
    public int cost;
    public char[] aspects;
    Vector3 diff;
    Vector3 OriginalPos;

    // Use this for initialization
    void Start() {
		card = GetComponent<Card> ();
        //cost = transform.GetComponent<AddCardInformation>().card.cost;
		cost = card["cost"].ToObject<int>();
        //aspects = new char[transform.GetComponent<AddCardInformation>().card.aspects.Length];
		Table tAspects = card["aspects"].Table;
		aspects = new char[tAspects.Length];
        int i;
        for (i = 0; i < tAspects.Length; i++)
			aspects[i] = tAspects.Get(i+1).ToObject<string>()[0];

    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().canPlay && CanBePlayed && inHand)
            transform.position = new Vector3(transform.position.x, transform.parent.position.y + 10, transform.position.z);

        clone = Instantiate(illusionCard, transform.parent.parent) as GameObject;
		clone.GetComponent<CardInHand>().card = card;
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
				newCard.GetComponent<CardInHand>().card = card;
                newCard.transform.position = Hand.chosenSlot.transform.position;
               
                newCard.GetComponent<CardInTable>().onTable = true;
				if (card["type"].ToObject<string>()[0] == 'a') {
                    int i;
                    if (Hand.chosenSlot.transform.childCount > 0) {
                        ArrayList aspectsToRemove = Board.atm.GetComponent<CardInTable>().aspectsToGive;

                        for (i = 0; i < aspectsToRemove.Count; i++) {
                            if (Board.player[0].GetComponent<PlayerStatus>().OwnAspects.Contains(aspectsToRemove[i]))
                                Board.player[0].GetComponent<PlayerStatus>().OwnAspects.Remove(aspectsToRemove[i]);

                            if (Board.player[1].GetComponent<PlayerStatus>().OwnAspects.Contains(aspectsToRemove[i]))
                                Board.player[1].GetComponent<PlayerStatus>().OwnAspects.Remove(aspectsToRemove[i]);
                        }
                        Destroy(Hand.chosenSlot.transform.GetChild(0).gameObject);
                    }
                    Board.atm = newCard;
                    newCard.transform.SetParent(Hand.chosenSlot.transform);
                    ArrayList aspectsToAdd = transform.GetComponent<CardInTable>().aspectsToGive;

                    for (i = 0; i < aspectsToAdd.Count; i++) {
                        if (!Board.player[0].GetComponent<PlayerStatus>().OwnAspects.Contains(aspectsToAdd[i]))
                            Board.player[0].GetComponent<PlayerStatus>().OwnAspects.Add(aspectsToAdd[i]);

                        if (!Board.player[1].GetComponent<PlayerStatus>().OwnAspects.Contains(aspectsToAdd[i]))
                            Board.player[1].GetComponent<PlayerStatus>().OwnAspects.Add(aspectsToAdd[i]);
                    }

				} else if (card["type"].ToObject<string>()[0] == 't') {
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

                    

                    ArrayList aspectsToAdd = GetComponent<CardInTable>().aspectsToGive;
                    ArrayList PlayerAspectsList = Board.player[Board.currPlayer-1].GetComponent<PlayerStatus>().OwnAspects;

                    for (i = 0; i < aspectsToAdd.Count; i++) {
                        if (!PlayerAspectsList.Contains(aspectsToAdd[i]))
                            PlayerAspectsList.Add(aspectsToAdd[i]);
                    }
				} else if (card["type"].ToObject<string>()[0] == 'c') {
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
        if (inHand) {
            if (cost <= transform.parent.parent.GetComponent<PlayerStatus>().mana) {
                CanBePlayed = true;
                transform.Find("Cost").GetComponent<Text>().color = Color.white;
                int i;
                for (i = 0; i < aspects.Length; i++) {
                    if (transform.parent.parent.GetComponent<PlayerStatus>().OwnAspects.Contains(aspects[i])) {
                        CanBePlayed = true;
                        transform.Find("Aspects").Find(aspects[i].ToString()).GetComponent<Image>().color = Color.white;
                    } else {
                        if (Board.AtmAspects.Contains(aspects[i])) {
                            CanBePlayed = true;
                            transform.Find("Aspects").Find(aspects[i].ToString()).GetComponent<Image>().color = Color.white;
                        } else {
                            CanBePlayed = false;
                            transform.Find("Aspects").Find(aspects[i].ToString()).GetComponent<Image>().color = Color.black;
                        }
                    }
                }

            } else {
                CanBePlayed = false;
                transform.Find("Cost").GetComponent<Text>().color = Color.red;

            }

        }
    }

}
