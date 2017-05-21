using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptSlot : MonoBehaviour {

    public int pos;
    public int playerIndex;
    public static float captX;

    // Update is called once per frame
    void Update() {
        if (CaptControl.draggingCapt) {
            if (Mathf.Abs(transform.position.x-captX) <= 42.5f && playerIndex == Board.currPlayer)
                CaptControl.captSlot = transform.gameObject;
        }
    }
}
