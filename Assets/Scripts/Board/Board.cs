using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

public class Board : MonoBehaviour {

    public Script luaEnv;
    public int currPlayer = 1;
    public GameObject[] players;
    public GameObject slot;
    public Vector3 mousePosition;
    public GameObject dragCard;

    void Awake() {
        luaEnv = new Script();
    }

	// Use this for initialization
	void Start () {
        players = new GameObject[2];
        players[0] = transform.Find("Player1").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
