using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardResourceGenerator : MonoBehaviour {

    public int manaToGive;
    public ArrayList aspectsToGive=new ArrayList();
    public bool canFarm = false;

	// Update is called once per frame
	void Update () {
		if (canFarm) {
            Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().mana += manaToGive;
            canFarm = false;
        }
	}
}
