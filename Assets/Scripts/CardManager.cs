using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardManager : MonoBehaviour {

    public Canvas canvas;

    List<CardScriptableObject> deck = new List<CardScriptableObject>();
    int deckSize = 0;
    List<GameObject> hand = new List<GameObject>();
    int handSize = 0;
    public GameObject card;
    public CardScriptableObject cartaGuaxininja;
    public CardScriptableObject cartaYoko;

    bool isDeckFinished = false;


    // Use this for initialization
    void Start () {

        print("BUILD YOUR DECK:");
        print("LEFT CLICK TO ADD GUAXININJA");
        print("RIGHT CLICK TO ADD YOKO");
        print("SPACE BAR TO FINISH BUILDING");

        /*deckSize = BuildDeck(deck);

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
        }*/
    }

    void Update()
    {
        if (!isDeckFinished)
        {
            if(deckSize == 30)
            {
                print("Deck size limit reached!");
                isDeckFinished = true;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(deckSize == 0)
                {
                    print("Your deck must have at least one card!");
                }
                else
                {
                    print("Deck building finished");
                    for (int i = 0; i < deckSize; i++)
                    {
                        CardScriptableObject temp;
                        int randSpace = Random.Range(0, deckSize);
                        temp = deck[randSpace];
                        deck[randSpace] = deck[i];
                        deck[i] = temp;
                    }
                    isDeckFinished = true;
                }
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                print("Added Guaxininja!");
                deck.Add(cartaGuaxininja);
                deckSize++;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                print("Added Yoko!");
                deck.Add(cartaYoko);
                deckSize++;
            }
        }
        if (isDeckFinished)
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
            if(Input.GetButtonDown("Fire2") && handSize > 0)
            {
                print("banana");
                Destroy(hand[0]);
                hand.RemoveAt(0);
                handSize--;
                //shift hand to left
            }
        }
    }

    public static IEnumerator WaitInput(bool wait)
    {
        while (wait)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                print("Fire1");
                wait = false;
            }
            
            yield return null;
        }
    }
}
