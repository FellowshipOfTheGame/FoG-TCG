using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptControl : MonoBehaviour {

    public int pos = 2;
    public static GameObject captSlot=null;
    public int playerIndex;
    float diff;
    float[] mouseDist;
    Color slotColor;
    public static bool draggingCapt = false;

    // Use this for initialization
    void Start() {
        mouseDist = new float[2];
        slotColor = transform.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update() {
        mouseDist[0] = Input.mousePosition.x - transform.position.x;
        mouseDist[1] = Input.mousePosition.y - transform.position.y;

        if (!draggingCapt) {
            if (Mathf.Abs(mouseDist[0]) <= 25 && Mathf.Abs(mouseDist[1]) <= 137 && Board.player[Board.currPlayer-1].GetComponent<PlayerStatus>().canMove && !Slot.choosingPlace) {
                transform.GetComponent<Image>().color = new Color(slotColor.r + 0.1f, slotColor.g + 0.1f, slotColor.b + 0.1f);

                if (Input.GetMouseButtonDown(0))
                    DragCaptStart();

            } else {
                transform.GetComponent<Image>().color = new Color(slotColor.r, slotColor.g, slotColor.b);
            }

        } else {
            DragCapt();

            if (Input.GetMouseButtonUp(0))
                DragCaptEnd();
        }

    }

    void DragCaptStart() {
        draggingCapt = true;
        transform.SetParent(transform.parent.parent.parent);
        diff = transform.position.y - Input.mousePosition.y;
        CaptSlot.captDiff = new Vector3(0.0f, diff, 0.0f);
        captSlot = transform.parent.gameObject;
    }

    void DragCapt() {
        CaptSlot.captY = transform.position.y;
        switch (pos) {
            case 1:
                if (Input.mousePosition.y + diff <= 443 && Input.mousePosition.y + diff >= 351)
                    transform.position = new Vector3(transform.position.x, Input.mousePosition.y + diff, transform.position.z);
                else {
                    if (Input.mousePosition.y + diff > 443)
                        transform.position = new Vector3(transform.position.x, 443, transform.position.z);
                    else
                        transform.position = new Vector3(transform.position.x, 351, transform.position.z);
                }
                break;

            case 2:
                if (Input.mousePosition.y + diff <= 443 && Input.mousePosition.y + diff >= 259)
                    transform.position = new Vector3(transform.position.x, Input.mousePosition.y + diff, transform.position.z);
                else {
                    if (Input.mousePosition.y + diff > 443)
                        transform.position = new Vector3(transform.position.x, 443, transform.position.z);
                    else
                        transform.position = new Vector3(transform.position.x, 259, transform.position.z);
                }
                break;

            case 3:
                if (Input.mousePosition.y + diff <= 351 && Input.mousePosition.y + diff >= 259)
                    transform.position = new Vector3(transform.position.x, Input.mousePosition.y + diff, transform.position.z);
                else {
                    if (Input.mousePosition.y + diff > 351)
                        transform.position = new Vector3(transform.position.x, 351, transform.position.z);
                    else
                        transform.position = new Vector3(transform.position.x, 259, transform.position.z);
                }
                break;
        }
    }

    void DragCaptEnd() {
        transform.SetParent(captSlot.transform);
        draggingCapt = false;
        if (pos!=captSlot.GetComponent<CaptSlot>().pos) {
            pos = captSlot.GetComponent<CaptSlot>().pos;
            Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().canPlay = false;
            Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().canMove = false;
        }
    }
}