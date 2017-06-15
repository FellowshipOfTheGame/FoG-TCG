using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadTabs : MonoBehaviour {

    public Transform abas;
    public GameObject genericCard;
    GameObject newCard = null;

    public void LoadAllCards() {
		foreach (CardInformation card in GameData.Cards) {

			if (card.aspects.Count > 0) {
                switch (card.aspects[0]) {
                    case 'W':
						newCard = Instantiate(genericCard, abas.Find("Aba1").GetChild(0).GetChild(0));
                        newCard.GetComponent<AddCardInformation>().card = card;
                        break;

                    case 'E':
						newCard = Instantiate(genericCard, abas.Find("Aba2").GetChild(0).GetChild(0));
                        newCard.GetComponent<AddCardInformation>().card = card;
                        break;

                    case 'F':
						newCard = Instantiate(genericCard, abas.Find("Aba3").GetChild(0).GetChild(0));
                        newCard.GetComponent<AddCardInformation>().card = card;
                        break;

                    case 'A':
						newCard = Instantiate(genericCard, abas.Find("Aba4").GetChild(0).GetChild(0));
                        newCard.GetComponent<AddCardInformation>().card = card;
                        break;
                }
			} else if(card.aspectsToGive.Count>0) {
                switch (card.aspectsToGive[0]) {
                    case 'W':
						newCard = Instantiate(genericCard, abas.Find("Aba1").GetChild(0).GetChild(0));
                        newCard.GetComponent<AddCardInformation>().card = card;
                        break;

                    case 'E':
						newCard = Instantiate(genericCard, abas.Find("Aba2").GetChild(0).GetChild(0));
                        newCard.GetComponent<AddCardInformation>().card = card;
                        break;

                    case 'F':
						newCard = Instantiate(genericCard, abas.Find("Aba3").GetChild(0).GetChild(0));
                        newCard.GetComponent<AddCardInformation>().card = card;
                        break;

                    case 'A':
						newCard = Instantiate(genericCard, abas.Find("Aba4").GetChild(0).GetChild(0));
                        newCard.GetComponent<AddCardInformation>().card = card;
                        break;
                }
                
            }

			newCard = Instantiate(genericCard, abas.Find("Aba5").GetChild(0).GetChild(0));
			newCard.GetComponent<AddCardInformation>().card = card;

        }
    }
}
