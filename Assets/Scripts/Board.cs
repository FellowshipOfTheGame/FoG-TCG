using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour{

    public CardInformation[] deck1;
    public CardInformation[] deck2;
    public static int currPlayer=1;
    public static int[,] cardMatriz;
    public static GameObject[] player;
    public static GameObject[] capt;
    bool PlayerInTurn = false;
    public static float TurnStartTime;
    Text info;

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
        info = transform.FindChild("Menu").FindChild("Infos").GetComponent<Text>();
        cardMatriz = new int[4,5];
        int i;
        int j;
        for (i = 0; i < 4; i++) {
            for (j = 0; j < 5; j++)
                cardMatriz[i, j] = -1;
        }
        PlayerInTurn = true;
        TurnStartTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        info.text = "Curr Player:" + currPlayer + " (Time:" + Mathf.Floor(Time.time-TurnStartTime) + ")";

        if (PlayerInTurn) {
            if (Time.time - TurnStartTime >= 30)
                TurnChange();
        }

    }

    public static void TurnChange() {
        TurnStartTime = Time.time;
        if (currPlayer == 1) {
            player[0].SetActive(false);
            player[1].SetActive(true);
            capt[0].GetComponent<CaptControl>().enabled = false;
            capt[1].GetComponent<CaptControl>().enabled = true;
            player[1].GetComponent<PlayerStatus>().canPlay = true;
            player[1].GetComponent<PlayerStatus>().canMove = true;
            player[1].GetComponent<PlayerStatus>().canBuy = true;
            currPlayer = 2;
        } else {
            player[1].SetActive(false);
            player[0].SetActive(true);
            capt[1].GetComponent<CaptControl>().enabled = false;
            capt[0].GetComponent<CaptControl>().enabled = true;
            player[0].GetComponent<PlayerStatus>().canPlay = true;
            player[0].GetComponent<PlayerStatus>().canMove = true;
            player[0].GetComponent<PlayerStatus>().canBuy = true;
            currPlayer = 1;
        }
    }

    void LoadDeck(CardInformation[] deck,string player) {
        int i;
        for (i = 0; i < deck.Length; i++)
            transform.FindChild(player).GetComponent<Deck>().deckList.Add(deck[i]);       
    }
}