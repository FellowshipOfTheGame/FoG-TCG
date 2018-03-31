using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MoonSharp.Interpreter;

[MoonSharpUserData]
public class Board : MonoBehaviour {

    public float time;
    public float limit;
    public float initialHand;
    public bool loadDeckFromMenu;
    public int maxHP;
    public float critic;
    [Space(5)]
    public AudioSource audioPlayer;
    public AudioClip startMusic;
    public AudioClip endMusic;
    [Space(5)]
    public Script luaEnv;
    [HideInInspector] public int currPlayer = 1;
    public Player[] players;
    [HideInInspector] public GameObject[,] cardMatrix = new GameObject[4,5];
    public GameObject cardAtm;
    [HideInInspector] public GameObject slot;
    public Vector3 mousePosition;
    [HideInInspector] public GameObject dragCard;
    public Transform illusionPos;
    public Transform illusionPos2;
    [HideInInspector] public MiniMenu miniMenu = null;
    [HideInInspector] public ResourceData data;
    Vector3 playerPosition;
    public GameObject GameOverScr;
    int counter = 1;
    public static int winner = 0; //winner = 0 -> ninguem venceu; winner = 1  -> player 1 venceu; winner = 2 -> player 2 venceu

    public static Card GetCardFromObject(DynValue obj)
    {
        return obj.ToObject<GameObject>().GetComponent<Card>();
    }

    public void AddAspects(int playerIndex, int[] aspects) {
        for (int i = 0; i < 4; i++)
            players[playerIndex - 1].aspects[i] += aspects[i];
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

    public void Print(DynValue val)
    {
        print(val.ToString());
    }

    void Awake()
    {
        UserData.RegisterAssembly();
        luaEnv = new Script();
        luaEnv.Globals["GetCard"] = (Func<DynValue, Card>) (obj => { return GetCardFromObject(obj); }) ;
        luaEnv.Globals["print"] = (Action<DynValue>) (obj => { Print(obj); });
        data = this.GetComponent<ResourceData>();
    }

    void SetPlayer(int index) {
        players[index - 1].HP = maxHP;
        players[index - 1].mana = 1;
        if (loadDeckFromMenu && GameManager.chosenDeck != null) {
            for (int i=0; i < GameManager.chosenDeck.Count; i++)
                players[index - 1].deckList.Add(GameManager.chosenDeck[i]);
        } else {
            for (int i = 0; i < players[index - 1].originalDeck.Count; i++)
                players[index - 1].deckList.Add(players[index - 1].originalDeck[i]);
        }
    }

    void SwitchPlayerState(int index, bool newState) {
        players[index - 1].gameObject.SetActive(newState);
        players[index - 1].capt.canMove = newState;
        players[index - 1].capt.canGenerate = newState;
        players[index - 1].capt.canBuy = newState;

        if (newState)
            players[index - 1].transform.position = playerPosition;
        else
            players[index - 1].transform.position = new Vector3(playerPosition.x, playerPosition.y, 0.0f);
    }

    // Use this for initialization
    void Start () {
        players = new Player[2];
        players[0] = transform.Find("Player1").GetComponent<Player>();
        players[1] = transform.Find("Player2").GetComponent<Player>();
        playerPosition = players[0].transform.position;
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

    public void EndGame() {
        Time.timeScale = 0;
        GameOverScr.SetActive(true);
        GameOverScr.GetComponent<GameOverScreen>().Show();
    }

    public void ResetGame() {
        Time.timeScale = 1;
        GameOverScr.SetActive(false);
        SetPlayer(1);
        SetPlayer(2);
        SwitchPlayerState(1, true);
        SwitchPlayerState(2, false);

        int i, j;
        for (i = 0; i < 4; i++) {
            for (j = 0; j < 5; j++) {
                if (cardMatrix[i, j] != null)
                    Destroy(cardMatrix[i, j]);

                cardMatrix[i, j] = null;
            }
        }

        time = 0.0f;
        for (i = players[0].transform.childCount - 1; i>=0; i--)
            Destroy(players[0].transform.GetChild(i).gameObject);

        players[0].transform.DetachChildren();

        for (i = players[1].transform.childCount - 1; i >= 0; i--)
            Destroy(players[1].transform.GetChild(i).gameObject);

        players[1].transform.DetachChildren();

        for (i = 1; i <= initialHand; i++) {
            players[0].PickUpCard();
            players[1].PickUpCard();
        }

        currPlayer = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (time > limit) {
            EndTurn();
        }
        time += Time.deltaTime;
	}

    public void changeMusic(bool isEnd) {
        if (isEnd && audioPlayer.clip == startMusic) {
            audioPlayer.PlayOneShot(endMusic);
            audioPlayer.volume = 0.5f;
        }
        if (!isEnd && audioPlayer.clip == endMusic) {
            audioPlayer.PlayOneShot(startMusic);
            audioPlayer.volume = 1.0f;
        }
    }

    public void CallCardPlacedEvents(Card c) {
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 5; j++) {
                if (cardMatrix[i, j] != null && cardMatrix[i, j] != c.gameObject)
                    cardMatrix[i, j].GetComponent<Card>().OnNewCardInField(c);
            }
        }
        if (cardAtm != null)
            cardAtm.GetComponent<Card>().OnNewCardInField(c);
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
            Destroy(illusionPos.GetChild(0).gameObject);
        }

        if (illusionPos2.childCount > 0) {
            Destroy(illusionPos2.GetChild(0).gameObject);
        }

        if (miniMenu != null)
            miniMenu.DestroyMenu(true);

        if (currPlayer == 1) {
            EndPlayerTurn(0);
     
            SwitchPlayerState(1, false);
            SwitchPlayerState(2, true);
            currPlayer = 2;
            if (counter < 10)
                players[1].mana = counter + 1;
            else
                players[1].mana = 10;

            StartPlayerTurn(1);
        }else {
            EndPlayerTurn(1);

            SwitchPlayerState(2, false);
            SwitchPlayerState(1, true);
            currPlayer = 1;
            counter++;
            if (counter <= 10)
                players[0].mana = counter;
            else
                players[0].mana = 10;

            StartPlayerTurn(0);
        }
        time = 0;
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
