using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour {

    public int posX, posy;
    public bool IsFull;
    public static bool posSelected;

    void OnMouseOver() {
        if (Board.IsPlacingCard == true) {
            Board.PosSelected = true;
            Board.slot = transform.GetComponent<Slot>();
        }else{
            Board.PosSelected = false;
        }
    }

    void OnMouseExit(){
        Board.PosSelected = false;
    }
}
