using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

    public int[] pos;
    public static bool choosingPlace=false;
    public bool IsFull;
    float[] mouseDist;
    float[] limit;
    
    Color slotColor;
    public GameObject clone;
    public GameObject card = null;

    // Use this for initialization
    void Start () {
        mouseDist = new float[2];
        limit = new float[2];
        slotColor = transform.GetComponent<Image>().color;
    }
	
	// Update is called once per frame
	void Update () {
        limit[0] = 65.5f;
        limit[1] = 45;
        mouseDist[0] = Input.mousePosition.x - transform.position.x;
        mouseDist[1] = Input.mousePosition.y - transform.position.y;

        if (choosingPlace) {
            if (pos[0] != 4) {
                if (Mathf.Abs(mouseDist[0]) <= limit[0] && Mathf.Abs(mouseDist[1]) <= limit[1] && !IsFull && Hand.dragCard.GetComponent<AddCardInformation>().card.type != 'a') {

                    transform.GetComponent<Image>().color = new Color(slotColor.r + 0.1f, slotColor.g + 0.1f, slotColor.b + 0.1f);
                    Hand.chosenSlot = transform.gameObject;

                    //calcular fileira certa
                    if (Hand.dragCard.GetComponent<AddCardInformation>().card.type == 't') {
                        if ((Board.currPlayer == 1 && pos[0] != 0) || (Board.currPlayer == 2 && pos[0] != 3))
                            Hand.chosenSlot = null;

                    }
                    if (Hand.dragCard.GetComponent<AddCardInformation>().card.type == 'c')
                        if ((Board.currPlayer == 1 && pos[0] != 1) || (Board.currPlayer == 2 && pos[0] != 2))
                            Hand.chosenSlot = null;


                } else {
                    transform.GetComponent<Image>().color = new Color(slotColor.r, slotColor.g, slotColor.b);


                }
            }else {
                if (Mathf.Abs(mouseDist[0]) <= 45 && Mathf.Abs(mouseDist[1]) <= 65.5f && Hand.dragCard.GetComponent<AddCardInformation>().card.type == 'a') {
                    transform.GetComponent<Image>().color = new Color(slotColor.r + 0.1f, slotColor.g + 0.1f, slotColor.b + 0.1f);
                    Hand.chosenSlot = transform.gameObject;
                }else {
                    transform.GetComponent<Image>().color = slotColor;
                    Hand.chosenSlot = null;
                }
            }
        }else {
            bool PointerIn;

            if (pos[0] != 4 && Mathf.Abs(mouseDist[0]) <= limit[0] && Mathf.Abs(mouseDist[1]) <= limit[1]) 
                PointerIn = true;
            else if(pos[0] == 4 && Mathf.Abs(mouseDist[0]) <= limit[1] && Mathf.Abs(mouseDist[1]) <= limit[0]) 
                    PointerIn = true;
            else 
                PointerIn = false;
            
            if (PointerIn && IsFull) {
                if (card == null) {
                    if (pos[0]==4 && Board.hologram != null) {
                        Destroy(Board.hologram);
                        Board.hologram = null;
                    }
                    card = Instantiate(clone, transform.parent.parent) as GameObject;
                    card.GetComponent<AddCardInformation>().card = transform.GetChild(0).gameObject.GetComponent<AddCardInformation>().card;
                    
                    //calcular posicao do holograma
                    card.transform.position = transform.position;
                    if (pos[0] < 4)
                        card.transform.position += new Vector3(160.0f, 0.0f, 0.0f) * Mathf.Sign(1.5f - pos[0]);
                    else {
                        card.transform.position += new Vector3(140.0f, 0.0f, 0.0f) * Mathf.Sign(Board.currPlayer-1.5f);
                    }

                    if (pos[1]==0 || pos[1] == 4) 
                        card.transform.position += new Vector3(0.0f, 90.0f, 0.0f) * Mathf.Sign(pos[1] - 2);
                    else if(pos[1] == 1 || pos[1] == 3)
                        card.transform.position += new Vector3(0.0f, 45.0f, 0.0f) * Mathf.Sign(pos[1] - 2);

                    Board.hologram = card;
                }

                if (Input.GetMouseButtonDown(0) && transform.GetChild(0).GetComponent<AddCardInformation>().card.type == 'c') {
                    if (transform.GetChild(0).GetComponent<CardAttack>().canAttack)
                        transform.GetChild(0).gameObject.GetComponent<CardAttack>().Attack();
                    else
                        Debug.Log("Can't Attack!");
                }
            } else {
                if (card != null) {
                    Destroy(card);
                    card = null;
                }
            }
        }
    }

}
