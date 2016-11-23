using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour {
    Vector3 myPos;
    Vector3 diff;
    public static GameObject thisCard=null;
    public static bool IsSelected=false;
    public static bool CanPlace = false;
    public bool OnHand = true;
    public static Transform slot;

    void Start(){
        myPos = transform.position;
    }

    void Update(){
        Vector3 mouse = Input.mousePosition;
        if (OnHand){
            if (Mathf.Abs(mouse.x - myPos.x) <= 60 * transform.parent.localScale.x && Mathf.Abs(mouse.y - myPos.y) <= 90 * transform.parent.localScale.x){
                if (Input.GetMouseButtonDown(0) && !IsSelected){
                    thisCard = Instantiate(gameObject, gameObject.transform.parent.parent) as GameObject;
                    diff = transform.position - Input.mousePosition;
                    IsSelected = true;
                }
                if (!IsSelected)
                    transform.position = new Vector3(myPos.x, myPos.y + 10, myPos.z);
            }
            else
            {
                if (!IsSelected)
                    transform.position = new Vector3(myPos.x, myPos.y - 10, myPos.z);
            }

            if (IsSelected && thisCard!=null)
                thisCard.transform.position = Input.mousePosition + diff;
        
            if (Input.GetMouseButtonUp(0) && IsSelected){
                transform.position = new Vector3(myPos.x, myPos.y - 10, myPos.z);
                IsSelected = false;
                if (CanPlace){
                    thisCard.GetComponent<Hand>().OnHand = false;
                    thisCard.transform.SetParent(slot.transform);

                    if(slot.transform.parent.GetComponent<Field>().FieldIndex==1)
                        thisCard.transform.Rotate(Vector3.forward, 270);
                    else
                        thisCard.transform.Rotate(Vector3.forward, 90);

                    slot.GetComponent<Slot>().IsFull = true;
                    thisCard = null;
                    CanPlace = false;

                    if (Board.CurrPlayer == 1)
                        Board.CurrPlayer = 2;
                    else
                        Board.CurrPlayer = 1;

                    //Destroy(gameObject);
                }else{
                    Destroy(thisCard);
                }
            }
        }
    }
}