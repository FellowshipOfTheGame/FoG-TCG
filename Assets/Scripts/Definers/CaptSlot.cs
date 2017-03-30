using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptSlot : MonoBehaviour {

    public int pos;
    public static Vector3 captDiff;
    public int playerIndex;
    public static float captY;

    // Update is called once per frame
    void Update() {
        if (CaptControl.draggingCapt) {
            if (Mathf.Abs(transform.position.y-captY) <= 45 && playerIndex == Board.currPlayer)
                CaptControl.captSlot = transform.gameObject;
        }
    }
}
