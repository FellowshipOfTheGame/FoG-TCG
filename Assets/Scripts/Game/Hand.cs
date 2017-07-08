using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {

    //comprar cartas
    Deck playerDeck;
    public GameObject genericCard;

    //jogar cartas no campo
    public GameObject chosenCard = null;
    public static GameObject dragCard = null;
    public static GameObject chosenSlot = null;

	// Use this for initialization
	void Start () {
        playerDeck = transform.parent.GetComponent<Deck>();
    }

    // Update is called once per frame
    void Update () {
      
        if (playerDeck.deckList.Count == 0)
            transform.parent.GetComponent<PlayerStatus>().canBuy = false;
	}

 
	/*
    //pegar uma carta aleatoria do deck
    public void PickUpCard() {
        if (transform.childCount < 7) {
            //escolhe uma carta; põe na mão; tira do deck
            int num = Random.Range(0, playerDeck.deckList.Count - 1);
            GameObject newCard = Instantiate(genericCard, transform);
			// TODO carregar script lua
            //newCard.GetComponent<AddCardInformation>().card = playerDeck.deckList[num];
            newCard.GetComponent<CardInHand>().inHand = true;
            playerDeck.deckList.RemoveAt(num);
            newCard = null;
        }
    }*/
	public string PickUpCard() {
		if (transform.childCount < 7) {
			//escolhe uma carta; põe na mão; tira do deck
			int num = Random.Range(0, playerDeck.deckList.Count - 1);
			string name = playerDeck.deckList [num];
			playerDeck.deckList.RemoveAt(num);
			return name;
		}
		return null;
	}
}
