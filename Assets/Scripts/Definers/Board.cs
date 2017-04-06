using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour{

    public CardInformation[] deck1;
    public CardInformation[] deck2;
    public static int currPlayer=1;
    public static GameObject[,] cardMatriz;
    public static GameObject atm=null;
    public static ArrayList AtmAspects = new ArrayList();
    public static GameObject hologram=null;
    public static GameObject[] player;
    public static GameObject[] capt;
    public static float TurnStartTime;

    public float maxTime;

	// Use this for initialization
	void Start () {
        LoadDeck(deck1, "Player1");
        LoadDeck(deck2, "Player2");
        player = new GameObject[2];
        player[0] = transform.FindChild("Player1").gameObject;
        player[1] = transform.FindChild("Player2").gameObject;
        capt = new GameObject[2];
        capt[0] = transform.FindChild("Table").FindChild("Cmd1").FindChild("CaptSlot2").FindChild("Capt").gameObject;
        capt[1] = transform.FindChild("Table").FindChild("Cmd2").FindChild("CaptSlot2").FindChild("Capt").gameObject;
        player[0].GetComponent<PlayerStatus>().canPlay = true;
        player[0].GetComponent<PlayerStatus>().canMove = true;
        player[0].GetComponent<PlayerStatus>().canBuy = true;
        player[1].SetActive(false);
        capt[1].GetComponent<CaptControl>().enabled = false;
  
        cardMatriz = new GameObject[4,5];
        int i;
        int j;
        for (i = 0; i < 4; i++) {
            for (j = 0; j < 5; j++)
                cardMatriz[i, j] = null;
        }
        TurnStartTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        capt[0].transform.FindChild("Text").GetComponent<Text>().text = Board.player[0].GetComponent<PlayerStatus>().HP.ToString();
        capt[1].transform.FindChild("Text").GetComponent<Text>().text = Board.player[1].GetComponent<PlayerStatus>().HP.ToString();
        transform.FindChild("Menu").FindChild("PlayerIndex").GetChild(0).GetComponent<Text>().text = "Player: " + currPlayer.ToString();
        transform.FindChild("Menu").FindChild("ManaCount").GetChild(0).GetComponent<Text>().text = "Mana: " + player[currPlayer - 1].GetComponent<PlayerStatus>().mana.ToString();

        if(maxTime + 1 - Time.time + TurnStartTime >= 10)
            transform.FindChild("Menu").FindChild("TimeShow").GetChild(0).GetComponent<Text>().text = Mathf.Floor(maxTime + 1 - Time.time + TurnStartTime).ToString();
        else
            transform.FindChild("Menu").FindChild("TimeShow").GetChild(0).GetComponent<Text>().text = "0" + Mathf.Floor(maxTime + 1 - Time.time + TurnStartTime).ToString();

        if (Time.time - TurnStartTime >= maxTime)
            TurnChange();
        
    }

    public static void TurnChange() {
        TurnStartTime = Time.time;
        if (Hand.dragCard != null) {
            Destroy(Hand.dragCard);
            Hand.dragCard = null;
            Hand.chosenSlot = null;
            Slot.choosingPlace = false;
        }

        if (currPlayer == 1) {
            player[0].SetActive(false);
            player[1].SetActive(true);
            capt[0].GetComponent<CaptControl>().enabled = false;
            capt[1].GetComponent<CaptControl>().enabled = true;
            player[1].GetComponent<PlayerStatus>().canPlay = true;
            player[1].GetComponent<PlayerStatus>().canMove = true;
            player[1].GetComponent<PlayerStatus>().canBuy = true;
            int i;
            for (i = 0; i <= 4; i++) {
                if (cardMatriz[2, i] != null)
                    cardMatriz[2, i].GetComponent<CardAttack>().canAttack = true;
            }
            for (i = 3; i <= 4; i++) {
                if (cardMatriz[3, i] != null)
                    cardMatriz[3, i].GetComponent<CardResourceGenerator>().canFarm = true;
            }

            currPlayer = 2;
        } else {
            player[1].SetActive(false);
            player[0].SetActive(true);
            capt[1].GetComponent<CaptControl>().enabled = false;
            capt[0].GetComponent<CaptControl>().enabled = true;
            player[0].GetComponent<PlayerStatus>().canPlay = true;
            player[0].GetComponent<PlayerStatus>().canMove = true;
            player[0].GetComponent<PlayerStatus>().canBuy = true;
            int i;
            for (i = 0; i <= 4; i++) {
                if (cardMatriz[0, i] != null)
                    cardMatriz[0, i].GetComponent<CardResourceGenerator>().canFarm = true;
            }
            for (i = 0; i <= 4; i++) {
                if(cardMatriz[1, i]!=null)
                    cardMatriz[1, i].GetComponent<CardAttack>().canAttack = true;
            }

            currPlayer = 1;
        }
        AtmButton.open = false;
        Destroy(hologram);
        hologram = null;
    }

    void LoadDeck(CardInformation[] deck,string player) {
        int i;
        for (i = 0; i < deck.Length; i++)
            transform.FindChild(player).GetComponent<Deck>().deckList.Add(deck[i]);       
    }
}