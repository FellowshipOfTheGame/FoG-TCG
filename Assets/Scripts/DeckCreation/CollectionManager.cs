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
            UpdateCardQuantities();
            cardsList.Add(newCard);
        }
	}

    void UpdateCardQuantities()
    {
    
    }
}
