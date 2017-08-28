using System.Collections;
using System.Collections.Generic;
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
    Vector3 playerPosition;

    void Awake()
    {
        UserData.RegisterAssembly();
        luaEnv = new Script();
    }

	// Use this for initialization
	void Start () {
        players = new Player[2];
        players[0] = transform.Find("Player1").GetComponent<Player>();
        playerPosition = players[0].transform.position;
        players[1] = transform.Find("Player2").GetComponent<Player>();
        players[1].transform.position = new Vector3(playerPosition.x, playerPosition.y, 0.0f);
        illusionPos = this.transform.Find("Table").Find("IllusionPos");
        illusionPos2 = this.transform.Find("Table").Find("IllusionPos2");
        int i, j;
        for (i = 0; i < 4; i++) {
            for (j = 0; j < 5; j++)
                cardMatrix[i, j] = null;
        }
        time = 0.0f;
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

        if (currPlayer == 1) {
            EndPlayerTurn(0);

            players[0].gameObject.SetActive(false);
            players[0].capt.canMove = false;
            players[0].capt.canGenerate = false;
            players[0].capt.canBuy = false;
            players[0].transform.position = players[1].transform.position = new Vector3(playerPosition.x, playerPosition.y, 0.0f);

            players[1].gameObject.SetActive(true);
            players[1].capt.canMove = true;
            players[1].capt.canGenerate = true;
            players[1].capt.canBuy = true;
            players[1].transform.position = playerPosition;
            currPlayer = 2;

            StartPlayerTurn(1);
        }else {
            EndPlayerTurn(1);

            players[1].gameObject.SetActive(false);
            players[1].capt.canMove = false;
            players[1].capt.canGenerate = false;
            players[1].capt.canBuy = false;
            players[1].transform.position = players[1].transform.position = new Vector3(playerPosition.x, playerPosition.y, 0.0f);

            players[0].gameObject.SetActive(true);
            players[0].capt.canMove = true;
            players[0].capt.canGenerate = true;
            players[0].capt.canBuy = true;
            players[0].transform.position = playerPosition;
            currPlayer = 1;

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
