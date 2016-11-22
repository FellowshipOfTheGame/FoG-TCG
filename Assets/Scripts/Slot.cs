using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

    public int posX, posy;
    public bool IsFull=false;

    void Update(){
        Vector3 myPos = transform.position;
        Vector3 mouse = Input.mousePosition;

        if (Mathf.Abs(mouse.x - myPos.x) <= 60 && Mathf.Abs(mouse.y - myPos.y) <= 40 && Hand.IsSelected && !IsFull){
            Hand.CanPlace = true;
            Hand.slot = this.transform;
        }else{
            if (Hand.slot == this.transform){
                Hand.CanPlace = false;
                Hand.slot = null;
            }
        }
    }
}
