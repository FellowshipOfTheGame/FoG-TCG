using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{

    public int[] pos;
    public static bool choosingPlace=false;
    public bool IsFull;
    
    Color slotColor;
    public GameObject clone;
    public GameObject card = null;

    // Use this for initialization
    void Start () {
        IsFull = false;
        slotColor = transform.GetComponent<Image>().color;
        transform.GetComponent<Image>().color = new Color(slotColor.r, slotColor.g, slotColor.b, 0.0f);
    }
	
    public void OnPointerEnter(PointerEventData eventData) {
        if (Board.draggingCard) {
            if(pos[0]==3 && Hand.dragCard.GetComponent<AddCardInformation>().card.type == 'a') {
                transform.GetComponent<Image>().color = new Color(slotColor.r, slotColor.g, slotColor.b, slotColor.a);
                Hand.chosenSlot = transform.gameObject;
            }else if(!IsFull && pos[0] == Board.currPlayer && Hand.dragCard.GetComponent<AddCardInformation>().card.type != 'a') {
                transform.GetComponent<Image>().color = new Color(slotColor.r, slotColor.g, slotColor.b, slotColor.a);
                Hand.chosenSlot = transform.gameObject;
            }
        }else {
            if (IsFull) {
                card = Instantiate(clone, transform.parent.parent) as GameObject;
                card.GetComponent<AddCardInformation>().card = transform.GetChild(transform.childCount - 1).gameObject.GetComponent<AddCardInformation>().card;
                card.GetComponent<CanvasGroup>().blocksRaycasts = false;
                card.transform.position = transform.position + new Vector3(0.0f, 100.0f, 0.0f);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (IsFull) {
            if (transform.GetChild(0).GetComponent<CardInTable>().canAttack)
                transform.GetChild(0).gameObject.GetComponent<CardInTable>().Attack();
            else
                Debug.Log("Can't Attack!");
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.GetComponent<Image>().color = new Color(slotColor.r, slotColor.g, slotColor.b, 0.0f);
        if (Board.draggingCard) {
            if (pos[0] == 4)
                Hand.chosenSlot = null;
            else {
                if (Hand.chosenSlot == transform.gameObject)
                    Hand.chosenSlot = null;
            }
        }else {
            if (IsFull && card != null) {
                Destroy(card);
                card = null;
            }
        }
    }

    void Update() {
        if (!Board.draggingCard) {
            transform.GetComponent<Image>().color = new Color(slotColor.r, slotColor.g, slotColor.b, 0.0f);
        }
    }
}
