using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectionManager : MonoBehaviour {

    public GameObject card;
    public GameObject collectionArea;
    List<GameObject> cardsList;

	void Start () {
        foreach (var ci in PlayerData.collection)
        {
            GameObject newCard = (GameObject)Instantiate(card, collectionArea.transform);
            newCard.GetComponent<AddCardInformation>().quantity = ci.Value;
            //newCard.GetComponent<AddCardInformation>().card = ScriptableObject da carta
            newCard.GetComponent<AddCardInformation>().title.text = ci.Key.title; //temp
            UpdateCardQuantities();
            cardsList.Add(newCard);
        }
	}

    void UpdateCardQuantities()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {

            if (string.Compare(transform.GetChild(i).GetComponent<AddCardInformation>().card.title, transform.GetChild(i + 1).GetComponent<AddCardInformation>().card.title) == 0)
            {
                transform.GetChild(i).GetComponent<AddCardInformation>().quantity++;
            
                Destroy(transform.GetChild(i + 1).gameObject);
            }
        }
    }
}
