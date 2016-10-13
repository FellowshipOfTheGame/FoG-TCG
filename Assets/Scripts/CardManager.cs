using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardManager : MonoBehaviour {

    public Canvas canvas;

    List<CardScriptableObject> deck = new List<CardScriptableObject>();
    int deckSize = 30;
    List<GameObject> hand = new List<GameObject>();
    int handSize = 0;
    public GameObject card;
    public CardScriptableObject cartaGuaxininja;
    public CardScriptableObject cartaYoko;


    // Use this for initialization
    void Start () {

        for(int i = 0; i < 15; i++)
        {
            deck.Add(cartaGuaxininja);
            deck.Add(cartaYoko);
        }

        for(int i = 0; i < 30; i++)
        {
            CardScriptableObject temp;
            int randSpace = Random.Range(0, deckSize);
            temp = deck[randSpace];
            deck[randSpace] = deck[i];
            deck[i] = temp;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && deckSize > 0)
        {
            if (handSize < 10)
            {
                hand.Add((GameObject)Instantiate(card, Vector3.zero, Quaternion.identity, canvas.transform));
                hand[handSize].transform.SetParent(canvas.transform, false);
                if (handSize == 0)
                {
                    hand[handSize].transform.position = canvas.transform.position + new Vector3(-350, -200);
                }
                else
                {
                    hand[handSize].transform.position = hand[handSize - 1].transform.position + new Vector3(80, 0);
                }
                hand[handSize].GetComponent<AddCardInformation>().card = deck[0];
                deck.RemoveAt(0);
                deckSize--;
                handSize++;
            }
            else
            {
                deck.RemoveAt(0);
                deckSize--;
                print("Card discarded!");
            }

        }
    }
}
