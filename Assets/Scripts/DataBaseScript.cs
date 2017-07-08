using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataBaseScript : MonoBehaviour {

    public static ArrayList CardData = new ArrayList();
    public string[] CardPaths;
    public Transform abas1, abas2;
    public GameObject genericCard;
    GameObject newCard = null;

    void Awake() {
        CardPaths = Directory.GetFiles(Application.dataPath + "/ScriptableObjects","*.asset"); //pega todos os caminhos das cartas

        foreach (string filePath in CardPaths) {
            int index = filePath.LastIndexOf("/");
            string localPath = "Assets" + filePath.Substring(index);
  
            Object SO = UnityEditor.AssetDatabase.LoadAssetAtPath(localPath, typeof(CardInformation));
            
            if (SO != null) {
                CardInformation card = SO as CardInformation;
                CardData.Add(card);
            }
        }
        if (GameManager.currScene == 'm') {
            LoadAllCards(abas1);
            LoadAllCards(abas2);
        }
    }

    public void LoadAllCards(Transform abas) {
		/*
        foreach (CardInformation card in CardData) {
            if (card.aspects.Length > 0) {
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
            } else if(card.aspectsToGive.Length>0) {
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
        }*/
    }
}
