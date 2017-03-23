using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour{

    public CardInformation[] deck1;
    public CardInformation[] deck2;
    public static int currPlayer=1;
    public int[][] cardMatriz;
    public static GameObject player1 = null;
    public static GameObject capt1;
    public static GameObject player2 = null;
    public static GameObject capt2;
    Text info;

	// Use this for initialization
	void Start () {
        LoadDeck(deck1, "Player1");
        LoadDeck(deck2, "Player2");
        player1 = transform.FindChild("Player1").gameObject;
        capt1 = transform.FindChild("Table").FindChild("Cmd1").FindChild("CaptSlot2").FindChild("Capt").gameObject;
        player2 = transform.FindChild("Player2").gameObject;
        capt2 = transform.FindChild("Table").FindChild("Cmd2").FindChild("CaptSlot2").FindChild("Capt").gameObject;
        player2.SetActive(false);
        capt2.GetComponent<CaptControl>().enabled = false;
        info = transform.FindChild("Menu").FindChild("Infos").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (currPlayer == 1)
            info.text = "Curr Player:1 (Deck:" + player1.GetComponent<Deck>().deckList.Count + ")";
        else
            info.text = "Curr Player:2 (Deck:" + player2.GetComponent<Deck>().deckList.Count + ")";
    }

    public static void TurnChange() {
        if (currPlayer == 1) {
            player1.SetActive(false);
            player2.SetActive(true);
            capt1.GetComponent<CaptControl>().enabled = false;
            capt2.GetComponent<CaptControl>().enabled = true;
            capt2.GetComponent<CaptControl>().playerIndex = 2;
            currPlayer = 2;
        } else {
            player2.SetActive(false);
            player1.SetActive(true);
            capt2.GetComponent<CaptControl>().enabled = false;
            capt1.GetComponent<CaptControl>().enabled = true;
            capt1.GetComponent<CaptControl>().playerIndex = 1;
            currPlayer = 1;
        }
    }

    void LoadDeck(CardInformation[] deck,string player) {
        int i;
        for (i = 0; i < deck.Length; i++)
            transform.FindChild(player).GetComponent<Deck>().deckList.Add(deck[i]);
        
    }
}
