using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MoonSharp.Interpreter;

[MoonSharpUserData]
public class Board : MonoBehaviour {

    public Script luaEnv;
    public int currPlayer = 1;
    public Player[] players;
    public GameObject[,] cardMatrix = new GameObject[4,5];
    public GameObject cardAtm;
    public GameObject slot;
    public Vector3 mousePosition;
    public GameObject dragCard;
    public Transform illusionPos;
    public Transform illusionPos2;
    public float time;
    public float limit;
    public float initialHand;
    public MiniMenu miniMenu = null;
    Vector3 playerPosition;
    public bool loadDeckFromMenu;

    public static int winner = 0; //winner = 0 -> ninguem venceu; winner = 1  -> player 1 venceu; winner = 2 -> player 2 venceu

    public static Card GetCardFromObject(DynValue obj)
    {
        return obj.ToObject<GameObject>().GetComponent<Card>();
    }

    public Card GetMatrixValue(int x, int y)
    {
        //print("x " + x + " y " + y);
        if (cardMatrix[x, y] != null)
        {
            //print(cardMatrix[x, y]);
            return cardMatrix[x, y].GetComponent<Card>();
        }

        //print("null");
        return null;
    }

    void Awake()
    {
        UserData.RegisterAssembly();
        luaEnv = new Script();
        luaEnv.Globals["GetCard"] = (Func<DynValue, Card>) (obj => { return GetCardFromObject(obj); }) ;
    }

    void SetPlayer(int index) {
        players[index - 1] = transform.Find("Player" + index.ToString()).GetComponent<Player>();
        playerPosition = players[index - 1].transform.position;
        players[index - 1].mana = 1;
        if(loadDeckFromMenu && GameManager.chosenDeck != null)
            players[index - 1].deckList = GameManager.chosenDeck;
    }

    void SwitchPlayerState(int index, bool newState) {
        players[index - 1].gameObject.SetActive(newState);
        players[index - 1].capt.canMove = newState;
        players[index - 1].capt.canGenerate = newState;
        players[index - 1].capt.canBuy = newState;

        if (newState)
            players[0].transform.position = players[1].transform.position = playerPosition;
        else
            players[0].transform.position = players[1].transform.position = new Vector3(playerPosition.x, playerPosition.y, 0.0f);
    }

    // Use this for initialization
    void Start () {
        players = new Player[2];
        SetPlayer(1);
        SetPlayer(2);
        SwitchPlayerState(1, true);
        SwitchPlayerState(2, false);

        illusionPos = this.transform.Find("Table").Find("IllusionPos");
        illusionPos2 = this.transform.Find("Table").Find("IllusionPos2");
        int i, j;
        for (i = 0; i < 4; i++) {
            for (j = 0; j < 5; j++)
                cardMatrix[i, j] = null;
        }
        time = 0.0f;
        for (i = 1; i <= initialHand; i++) {
            players[0].PickUpCard();
            players[1].PickUpCard();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (time > limit) {
            EndTurn();
        }
        time += Time.deltaTime;
	}

    public void EndTurn() {
        if (Slot.isChoosingPlace) {
            Slot.isChoosingPlace = false;
            dragCard.transform.position = dragCard.GetComponent<CardClick>().originPos;
            dragCard.GetComponent<CardClick>().isDragging = false;
            dragCard.GetComponent<BoxCollider>().enabled = true;
            dragCard = null;

            if (slot != null) {
                slot.GetComponent<SpriteRenderer>().color = Color.clear;
                slot = null;
            }
        }

        if (illusionPos.childCount > 0) {
            GameObject elevatedCard = illusionPos.GetChild(0).GetComponent<IllusionScript>().original;
            Destroy(illusionPos.GetChild(0).gameObject);
        }

        if (illusionPos2.childCount > 0) {
            GameObject elevatedCard = illusionPos2.GetChild(0).GetComponent<IllusionScript>().original;
            Destroy(illusionPos2.GetChild(0).gameObject);
        }

        if (miniMenu != null)
            miniMenu.DestroyMenu(true);

        if (currPlayer == 1) {
            EndPlayerTurn(0);
     
            SwitchPlayerState(1, false);
            SwitchPlayerState(2, true);
            currPlayer = 2;

            StartPlayerTurn(1);
        }else {
            EndPlayerTurn(1);

            SwitchPlayerState(2, false);
            SwitchPlayerState(1, true);
            currPlayer = 1;

            StartPlayerTurn(0);
        }
        time = 0;
        if(players[currPlayer - 1].mana < 10)
            players[currPlayer - 1].mana++;

        players[currPlayer - 1].ResetTurn();
    }

    private void StartPlayerTurn(int player)
    {
        Board board = GameObject.FindObjectOfType<Board>();
        int[] rows = new int[] {player == 0 ? 0 : 3, player == 0 ? 1 : 2 };
        for (int i = 0; i < 5; i++)
            foreach (int j in rows)
            {
                GameObject obj = board.cardMatrix[j, i];
                if (obj != null)
                    obj.GetComponent<Card>().OnTurnStart();
            }
    }

    private void EndPlayerTurn(int player)
    {
        Board board = GameObject.FindObjectOfType<Board>();
        int[] rows = new int[] { player == 0 ? 0 : 3, player == 0 ? 1 : 2 };
        for (int i = 0; i < 5; i++)
            foreach (int j in rows)
            {
                GameObject obj = board.cardMatrix[j, i];
                if (obj != null)
                    obj.GetComponent<Card>().OnTurnEnd();
            }
    }
}
