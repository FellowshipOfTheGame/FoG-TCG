using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInHand : MonoBehaviour {

    bool CanBePlayed = false;
    GameObject clone;
    public int cost;
    public char[] aspects;
    float[] mouseDist;

    // Use this for initialization
    void Start() {
        mouseDist = new float[2];
        cost = transform.GetComponent<AddCardInformation>().card.cost;
        aspects = new char[transform.GetComponent<AddCardInformation>().card.aspects.Length];
        int i;
        for (i = 0; i < aspects.Length; i++)
            aspects[i] = transform.GetComponent<AddCardInformation>().card.aspects[i];

    }

    // Update is called once per frame
    void Update() {
        mouseDist[0] = Input.mousePosition.x - transform.position.x;
        mouseDist[1] = Input.mousePosition.y - transform.position.y;

        if (transform.parent.name == "Hand") {
            if (cost <= transform.parent.parent.GetComponent<PlayerStatus>().mana) {
                CanBePlayed = true;
                transform.FindChild("Cost").GetComponent<Text>().color = Color.white;
                int i;
                for (i = 0; i < aspects.Length; i++) {
                    if (transform.parent.parent.GetComponent<PlayerStatus>().OwnAspects.Contains(aspects[i])) {
                        CanBePlayed = true;
                        transform.FindChild("Aspects").FindChild(aspects[i].ToString()).GetComponent<Image>().color = Color.white;
                    } else {
                        if (Board.AtmAspects.Contains(aspects[i])) {
                            CanBePlayed = true;
                            transform.FindChild("Aspects").FindChild(aspects[i].ToString()).GetComponent<Image>().color = Color.white;
                        } else {
                            CanBePlayed = false;
                            transform.FindChild("Aspects").FindChild(aspects[i].ToString()).GetComponent<Image>().color = Color.black;
                        }
                    }
                }

            } else {
                CanBePlayed = false;
                transform.FindChild("Cost").GetComponent<Text>().color = Color.red;

            }

            if (Mathf.Abs(mouseDist[0]) <= 45 && Mathf.Abs(mouseDist[1]) <= 65.5f && Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().canPlay) {
                transform.position = new Vector3(transform.position.x, transform.parent.position.y + 10, transform.position.z);
                if (clone == null) {
                    clone = Instantiate(transform.parent.GetComponent<Hand>().illusionCard, transform.parent.parent) as GameObject;
                    clone.GetComponent<AddCardInformation>().card = transform.GetComponent<AddCardInformation>().card;
                    clone.transform.position = transform.position + new Vector3(0, 100, 0);
                }

                if (Input.GetMouseButtonDown(0) && CanBePlayed) {
                    Destroy(clone);
                    clone = null;
                    transform.parent.GetComponent<Hand>().DragCardStart(transform.gameObject);
                }
            } else {
                transform.position = new Vector3(transform.position.x, transform.parent.position.y, transform.position.z);
                if (clone != null) {
                    Destroy(clone);
                    clone = null;
                }
                    
            }
        }
    }

}
