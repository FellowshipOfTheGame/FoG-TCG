using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

    public int[] pos;
    public static bool choosingPlace=false;
    public bool IsFull;
    float[] mouseDist;
    Color slotColor;

	// Use this for initialization
	void Start () {
        mouseDist = new float[2];
        slotColor = transform.GetComponent<Image>().color;
    }
	
	// Update is called once per frame
	void Update () {
        mouseDist[0] = Input.mousePosition.x - transform.position.x;
        mouseDist[1] = Input.mousePosition.y - transform.position.y;
        if (choosingPlace) {
            if (Mathf.Abs(mouseDist[0]) <= 65.5f && Mathf.Abs(mouseDist[1]) <= 45 && !IsFull) {
                transform.GetComponent<Image>().color = new Color(slotColor.r + 0.1f, slotColor.g + 0.1f, slotColor.b + 0.1f);

                //calcular fileira certa
                if (Hand.dragCard.GetComponent<AddCardInformation>().card.type == 't') {
                    if (Board.currPlayer == 1 && pos[0] == 0)
                        Hand.chosenSlot = transform.gameObject;
                    else {
                        if (Board.currPlayer == 2 && pos[0] == 3)
                            Hand.chosenSlot = transform.gameObject;
                        else {
                            Hand.chosenSlot = null;
                        }
                    }
                }
                if (Hand.dragCard.GetComponent<AddCardInformation>().card.type == 'c') {
                    if (Board.currPlayer == 1 && pos[0] == 1)
                        Hand.chosenSlot = transform.gameObject;
                    else {
                        if (Board.currPlayer == 2 && pos[0] == 2)
                            Hand.chosenSlot = transform.gameObject;
                        else {
                            Hand.chosenSlot = null;
                        }
                    }
                }

            } else {
                if (Hand.chosenSlot == transform.gameObject)
                    Hand.chosenSlot = null;

                transform.GetComponent<Image>().color = new Color(slotColor.r, slotColor.g, slotColor.b);
            }
        } else {
            transform.GetComponent<Image>().color = new Color(slotColor.r, slotColor.g, slotColor.b);
        }
            

    }
}
