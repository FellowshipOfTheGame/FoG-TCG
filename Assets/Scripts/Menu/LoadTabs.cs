using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadTabs : MonoBehaviour {

	public Transform abas;
	public GameObject genericCard;
	public GameObject dunnoCard;
    GameObject newCard = null;

    bool loaded;

    void Start(){
        loaded = false;
    }

	private void DeleteCardsFromTabs() {
		for(int i = 1; i <= 5; i++){
			Transform abax = abas.Find ("Aba"+i).GetChild (0).GetChild (0);
			foreach (Transform child in abax.transform) {
				GameObject.Destroy(child.gameObject);
			}
		}
	}

	private void LoadCard(CardInformation card, GameObject prefab, bool addInfo) {
        bool onTab = false;
        if (card.aspects[0] > 0 || card.aspectsToGive[0] > 0) {
            newCard = Instantiate(prefab, abas.Find("Aba1").GetChild(0).GetChild(0));
            if (addInfo) newCard.GetComponent<AddCardInformation>().card = card;
            onTab = true;
        }
        if (card.aspects[1] > 0 || card.aspectsToGive[1] > 0) {
            newCard = Instantiate(prefab, abas.Find("Aba2").GetChild(0).GetChild(0));
            if (addInfo) newCard.GetComponent<AddCardInformation>().card = card;
            onTab = true;
        }
        if (card.aspects[2] > 0 || card.aspectsToGive[2] > 0) {
            newCard = Instantiate(prefab, abas.Find("Aba3").GetChild(0).GetChild(0));
            if (addInfo) newCard.GetComponent<AddCardInformation>().card = card;
            onTab = true;
        }
        if (!onTab) {
            newCard = Instantiate(prefab, abas.Find("Aba4").GetChild(0).GetChild(0));
            if (addInfo) newCard.GetComponent<AddCardInformation>().card = card;
            onTab = true;
        }
        newCard = Instantiate(prefab, abas.Find("Aba5").GetChild(0).GetChild(0));
        if (addInfo) newCard.GetComponent<AddCardInformation>().card = card;
        onTab = true;
    }

    public void LoadAllCards() {
		
		DeleteCardsFromTabs ();

		foreach (CardInformation card in GameData.Cards) {
			LoadCard (card, genericCard, true);
        }
    }

	public void LoadBoughtCards() {

		DeleteCardsFromTabs ();

		foreach (CardInformation card in GameData.Cards) {
			if(card.qtdd > 0) {
				LoadCard (card, genericCard, true);
			}
		}
	}

	public void LoadGalery() {

		//DeleteCardsFromTabs ();
        if(!loaded){
            foreach (CardInformation card in GameData.Cards) {
                LoadCard (card, genericCard, true);
                
            }
            loaded = true;
        }
	}

}
