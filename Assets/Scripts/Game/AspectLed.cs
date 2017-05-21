using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectLed : MonoBehaviour {

    public int playerIndex;

	// Use this for initialization
	void Start () {
        int i;
        for (i = 0; i < 4; i++)
            transform.GetChild(0).GetComponent<Image>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Board.player[playerIndex - 1].GetComponent<PlayerStatus>().OwnAspects.Contains('F'))
            transform.Find("F").GetComponent<Image>().enabled = true;
        else
            transform.Find("F").GetComponent<Image>().enabled = false;

        if (Board.player[playerIndex - 1].GetComponent<PlayerStatus>().OwnAspects.Contains('W'))
            transform.Find("W").GetComponent<Image>().enabled = true;
        else
            transform.Find("W").GetComponent<Image>().enabled = false;

        if (Board.player[playerIndex - 1].GetComponent<PlayerStatus>().OwnAspects.Contains('E'))
            transform.Find("E").GetComponent<Image>().enabled = true;
        else
            transform.Find("E").GetComponent<Image>().enabled = false;

        if (Board.player[playerIndex - 1].GetComponent<PlayerStatus>().OwnAspects.Contains('A'))
            transform.Find("A").GetComponent<Image>().enabled = true;
        else
            transform.Find("A").GetComponent<Image>().enabled = false;
    }
}
