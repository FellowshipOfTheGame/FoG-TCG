using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CaptControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {

    public int pos = 2;
    public static GameObject captSlot=null;
    public int playerIndex;
    public float[] limits;
    float diff;
    Color slotColor;
    public static bool draggingCapt = false;

    // Use this for initialization
    void Start() {
        slotColor = transform.GetComponent<Image>().color;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!Slot.choosingPlace && Board.player[playerIndex - 1].GetComponent<PlayerStatus>().canMove) {
            transform.GetComponent<Image>().color = new Color(slotColor.r + 0.1f, slotColor.g + 0.1f, slotColor.b + 0.1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (!Slot.choosingPlace && Board.player[playerIndex - 1].GetComponent<PlayerStatus>().canMove && !draggingCapt) {
            transform.GetComponent<Image>().color = new Color(slotColor.r, slotColor.g, slotColor.b);
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if(Board.player[playerIndex - 1].GetComponent<PlayerStatus>().canMove) {
            draggingCapt = true;
            diff = transform.position.x - Input.mousePosition.x;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (draggingCapt) {
            CaptSlot.captX = transform.position.x;
            switch (pos) {
                case 1:
                    if (Input.mousePosition.x + diff >= limits[0] && Input.mousePosition.x + diff <= limits[1])
                        transform.position = new Vector3(Input.mousePosition.x + diff, transform.position.y, transform.position.z);
                    else {
                        if (Input.mousePosition.x + diff < limits[0])
                            transform.position = new Vector3(limits[0], transform.position.y, transform.position.z);
                        else
                            transform.position = new Vector3(limits[1], transform.position.y, transform.position.z);
                    }
                    break;

                case 2:
                    if (Input.mousePosition.x + diff >= limits[0] && Input.mousePosition.x + diff <= limits[2])
                        transform.position = new Vector3(Input.mousePosition.x + diff, transform.position.y, transform.position.z);
                    else {
                        if (Input.mousePosition.x + diff < limits[0])
                            transform.position = new Vector3(limits[0], transform.position.y, transform.position.z);
                        else
                            transform.position = new Vector3(limits[2], transform.position.y, transform.position.z);
                    }
                    break;

                case 3:
                    if (Input.mousePosition.x + diff >= limits[1] && Input.mousePosition.x + diff <= limits[2])
                        transform.position = new Vector3(Input.mousePosition.x + diff, transform.position.y, transform.position.z);
                    else {
                        if (Input.mousePosition.x + diff < limits[1])
                            transform.position = new Vector3(limits[1], transform.position.y, transform.position.z);
                        else
                            transform.position = new Vector3(limits[2], transform.position.y, transform.position.z);
                    }
                    break;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventdata) {
        if (draggingCapt) {
            transform.SetParent(captSlot.transform);
            draggingCapt = false;
            if (pos != captSlot.GetComponent<CaptSlot>().pos) {
                pos = captSlot.GetComponent<CaptSlot>().pos;
                Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().canPlay = false;
                Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().canMove = false;
            }
            transform.GetComponent<Image>().color = new Color(slotColor.r, slotColor.g, slotColor.b);
        }
    }
}