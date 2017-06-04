using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectLed : MonoBehaviour {

    public int playerIndex;
	
	// Update is called once per frame
	void Update () {
            transform.Find("F").GetComponent<Text>().text = Board.player[playerIndex - 1].GetComponent<PlayerStatus>().OwnAspects[0].ToString();
            transform.Find("W").GetComponent<Text>().text = Board.player[playerIndex - 1].GetComponent<PlayerStatus>().OwnAspects[1].ToString();
            transform.Find("E").GetComponent<Text>().text = Board.player[playerIndex - 1].GetComponent<PlayerStatus>().OwnAspects[2].ToString();
            transform.Find("A").GetComponent<Text>().text = Board.player[playerIndex - 1].GetComponent<PlayerStatus>().OwnAspects[3].ToString();
    }
}
