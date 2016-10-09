using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardManager : MonoBehaviour {

    public Canvas canvas;

    List<CardScriptableObject> deck = new List<CardScriptableObject>();
    int deckSize = 10;
    List<GameObject> hand = new List<GameObject>();
    int handSize = 0;
    public GameObject card;
    public CardScriptableObject cartaGuaxininja;
    public CardScriptableObject cartaYoko;


    // Use this for initialization
    void Start () {

        for(int i = 0; i < 5; i++)
        {
            deck.Add(cartaGuaxininja);
            deck.Add(cartaYoko);
        }

        for(int i = 0; i < 10; i++)
        {
            CardScriptableObject temp;
            int randSpace = Random.Range(0, deckSize);
            temp = deck[randSpace];
            deck[randSpace] = deck[i];
            deck[i] = temp;
        }

        /*
        GameObject panel1 = (GameObject)Instantiate(card, Vector3.zero, Quaternion.identity, canvas.transform);
        panel1.transform.SetParent(canvas.transform, false);
        panel1.transform.position = canvas.transform.position + new Vector3(75, 0);
        panel1.GetComponent<AddCardInformation>().card = cartaGuaxininja;

        GameObject panel2 = (GameObject)Instantiate(card, Vector3.zero, Quaternion.identity, canvas.transform);
        panel2.transform.SetParent(canvas.transform, false);
        panel2.transform.position = canvas.transform.position + new Vector3(-75, 0);
        panel2.GetComponent<AddCardInformation>().card = cartaYoko;
        */
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && deckSize > 0)
        {
            hand.Add((GameObject)Instantiate(card, Vector3.zero, Quaternion.identity, canvas.transform)); 
            hand[handSize].transform.SetParent(canvas.transform, false);
            if (handSize == 0)
            {
                hand[handSize].transform.position = canvas.transform.position + new Vector3(-350, 0);
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
    }
}
