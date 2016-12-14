using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectionManager : MonoBehaviour {

    public GameObject card;
    public GameObject minimizedCard;

    public Transform deckListManager;
    public GameObject collectionZone;
    public GameObject deckList;

    public Canvas canvas;

    List<GameObject> cardsList;

	void Start () {

        foreach (var ci in PlayerData.collection)
        {
            GameObject newCard = (GameObject)Instantiate(card, collectionZone.transform);
            newCard.GetComponent<CollectionDraggable>().deckListManager = deckList.GetComponent<DeckListManager>();
            newCard.GetComponent<CollectionDraggable>().canvas = canvas;
            newCard.GetComponent<CollectionDraggable>().collectionZone = collectionZone;
            newCard.GetComponent<CollectionDraggable>().deckListZone = deckList;
            newCard.GetComponent<CollectionDraggable>().deckList = deckList;
            newCard.GetComponent<CollectionDraggable>().minimizedCard = minimizedCard;

            RectTransform rt = newCard.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(120, 180);
            rt.localScale = Vector3.one;
            newCard.GetComponent<AddCardInformation>().quantity = ci.Value;
            newCard.GetComponent<AddCardInformation>().card = ci.Key.card;
        }

        UpdateCardQuantities();
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
