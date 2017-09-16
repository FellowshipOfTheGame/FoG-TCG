using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadTabs : MonoBehaviour {

	public Transform abas;
	public GameObject genericCard;
	public GameObject dunnoCard;
    GameObject newCard = null;

	private void DeleteCardsFromTabs() {
		for(int i = 1; i <= 6; i++){
			Transform abax = abas.Find ("Aba"+i).GetChild (0).GetChild (0);
			foreach (Transform child in abax.transform) {
				GameObject.Destroy(child.gameObject);
			}
		}
	}

	private void LoadCard(CardInformation card, GameObject prefab, bool addInfo) {

		if (card.aspects.Count > 0) {
			switch (card.aspects[0]) {
			case 'W':
				newCard = Instantiate(prefab, abas.Find("Aba1").GetChild(0).GetChild(0));
				if(addInfo) newCard.GetComponent<AddCardInformation>().card = card;
				break;

			case 'E':
				newCard = Instantiate(prefab, abas.Find("Aba2").GetChild(0).GetChild(0));
				if(addInfo) newCard.GetComponent<AddCardInformation>().card = card;
				break;

			case 'F':
				newCard = Instantiate(prefab, abas.Find("Aba3").GetChild(0).GetChild(0));
				if(addInfo) newCard.GetComponent<AddCardInformation>().card = card;
				break;

			case 'A':
				newCard = Instantiate(prefab, abas.Find("Aba4").GetChild(0).GetChild(0));
				if(addInfo) newCard.GetComponent<AddCardInformation>().card = card;
				break;
			}
		} else if(card.aspectsToGive.Count>0) {
			switch (card.aspectsToGive[0]) {
			case 'W':
				newCard = Instantiate(prefab, abas.Find("Aba1").GetChild(0).GetChild(0));
				if(addInfo) newCard.GetComponent<AddCardInformation>().card = card;
				break;

			case 'E':
				newCard = Instantiate(prefab, abas.Find("Aba2").GetChild(0).GetChild(0));
				if(addInfo) newCard.GetComponent<AddCardInformation>().card = card;
				break;

			case 'F':
				newCard = Instantiate(prefab, abas.Find("Aba3").GetChild(0).GetChild(0));
				if(addInfo) newCard.GetComponent<AddCardInformation>().card = card;
				break;

			case 'A':
				newCard = Instantiate(prefab, abas.Find("Aba4").GetChild(0).GetChild(0));
				if(addInfo) newCard.GetComponent<AddCardInformation>().card = card;
				break;
			}

		}

		newCard = Instantiate(prefab, abas.Find("Aba5").GetChild(0).GetChild(0));
		if(addInfo) newCard.GetComponent<AddCardInformation>().card = card;

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

		DeleteCardsFromTabs ();

		foreach (CardInformation card in GameData.Cards) {
			if (card.qtdd > 0) {
				LoadCard (card, genericCard, true);
			} else {
				LoadCard (card, dunnoCard, false);
			}
		}
	}

}
