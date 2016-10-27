using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardManager : MonoBehaviour {

    public Canvas canvas;
    public Text deckList;

    [HideInInspector]
    public List<CardInformation> database = new List<CardInformation>();

    public List<int> collection = new List<int>();

    List<CardInformation> deck = new List<CardInformation>();
    int deckSize = 0;
    List<GameObject> hand = new List<GameObject>();
    int handSize = 0;

    public GameObject cardPrefab;
    public CardInformation cartaGuaxininja;
    public CardInformation cartaYoko;

    bool isDeckFinished = false;


    // Use this for initialization
    void Start () {
        for(int i = 0; i < 10; i++)
        {
            collection[i] = 3;
        }
       
    }

    void Update()
    {
        if (!isDeckFinished)
        {
            if (deckSize == 30)
            {
                print("Deck size limit reached!");
                isDeckFinished = true;
            }
            else {
                if (Input.anyKeyDown)
                {
                    if (Input.inputString == " ")
                    {
                        if (deckSize == 0)
                        {
                            print("Your deck must have at least one card!");
                        }
                        else
                        {
                            print("Deck building finished");
                            for (int i = 0; i < deckSize; i++)
                            {
                                CardInformation temp;
                                int randSpace = Random.Range(0, deckSize);
                                temp = deck[randSpace];
                                deck[randSpace] = deck[i];
                                deck[i] = temp;
                            }
                            isDeckFinished = true;
                        }
                    }
                    else if (Input.inputString == "0" || Input.inputString == "1" || Input.inputString == "2" || Input.inputString == "3" || Input.inputString == "4" || Input.inputString == "5" || Input.inputString == "6" || Input.inputString == "7" || Input.inputString == "8" || Input.inputString == "9")
                    {
                        int keypressed = int.Parse(Input.inputString);
                        if (collection[keypressed] == 0)
                        {
                            Debug.Log("You can't add that card");
                        }
                        else
                        {
                            collection[keypressed]--;
                            deck.Add(database[keypressed]);
                            deckSize++;
                            Debug.Log(database[keypressed].title + " added");
                            UpdateDeckList();
                        }  
                    }
                }
            }
        }
        if (isDeckFinished)
        {
            if (Input.GetButtonDown("Fire1") && deckSize > 0)
            {
                if (handSize < 10)
                {
                    hand.Add((GameObject)Instantiate(cardPrefab, canvas.transform));
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
                Destroy(hand[0]);
                hand.RemoveAt(0);
                handSize--;
                for(int i = 0; i < handSize; i++)
                {
                    hand[i].transform.position = new Vector2(hand[i].transform.position.x - 80, hand[i].transform.position.y);
                }
            }
        }
    }

    void UpdateDeckList()
    {
        deckList.text = deckList.text + System.Environment.NewLine + deck[deckSize-1].title;
        //preciso dar sort em ordem alfabetica e adicionar "x2" ou "x3" em cartas repetidas
        
    }

    /*public static IEnumerator WaitInput(bool wait)
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
    }*/
}
