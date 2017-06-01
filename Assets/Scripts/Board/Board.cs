using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;

public sealed class Board : MonoBehaviour {

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
	public static bool draggingCard = false;

	public float maxTime;

	public BoardManager manager;
	public Script luaEnv;

	void InitLuaEnv() {
		luaEnv = new Script ();
	}

	// Use this for initialization
	void Awake () {
		InitLuaEnv ();

		LoadDeck(deck1, "Player1");
		LoadDeck(deck2, "Player2");
		player = new GameObject[2];
		player[0] = transform.Find("Player1").gameObject;
		player[1] = transform.Find("Player2").gameObject;
		capt = new GameObject[2];
		capt[0] = transform.Find("Table").Find("Cmd1").Find("CaptSlot2").Find("Capt").gameObject;
		capt[1] = transform.Find("Table").Find("Cmd2").Find("CaptSlot2").Find("Capt").gameObject;
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

		manager = gameObject.AddComponent<BoardManager>() as BoardManager;
		GameObject obj = new GameObject("Event Manager", typeof(EventManager));
		obj.transform.parent = gameObject.transform;
	}

	// Update is called once per frame
	void Update () {
		capt[0].transform.Find("Text").GetComponent<Text>().text = Board.player[0].GetComponent<PlayerStatus>().HP.ToString();
		capt[1].transform.Find("Text").GetComponent<Text>().text = Board.player[1].GetComponent<PlayerStatus>().HP.ToString();
		transform.Find("Menu").Find("PlayerIndex").GetChild(0).GetComponent<Text>().text = "Player: " + currPlayer.ToString();
		transform.Find("Menu").Find("ManaCount").GetChild(0).GetComponent<Text>().text = "Mana: " + player[currPlayer - 1].GetComponent<PlayerStatus>().mana.ToString();

		if(maxTime + 1 - Time.time + TurnStartTime >= 10)
			transform.Find("Menu").Find("TimeShow").GetChild(0).GetComponent<Text>().text = Mathf.Floor(maxTime + 1 - Time.time + TurnStartTime).ToString();
		else
			transform.Find("Menu").Find("TimeShow").GetChild(0).GetComponent<Text>().text = "0" + Mathf.Floor(maxTime + 1 - Time.time + TurnStartTime).ToString();

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
					cardMatriz[2, i].GetComponent<CardInTable>().canAttack = true;
			}
			for (i = 0; i <= 4; i++) {
				if (cardMatriz[3, i] != null)
					cardMatriz[3, i].GetComponent<CardInTable>().canFarm = true;
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
					cardMatriz[0, i].GetComponent<CardInTable>().canFarm = true;
			}
			for (i = 0; i <= 4; i++) {
				if(cardMatriz[1, i]!=null)
					cardMatriz[1, i].GetComponent<CardInTable>().canAttack = true;
			}

			currPlayer = 1;
		}
		Destroy(hologram);
		hologram = null;
	}

	void LoadDeck(CardInformation[] deck,string player) {
		int i;
		for (i = 0; i < deck.Length; i++)
			transform.Find(player).GetComponent<Deck>().deckList.Add(deck[i]);       
	}

}
