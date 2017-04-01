using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInHand : MonoBehaviour {

    bool CanBePlayed=false;
    public int cost;
    public char[] aspects;
    float[] mouseDist;

	// Use this for initialization
	void Start () {
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

        if (cost <= transform.parent.parent.GetComponent<PlayerStatus>().mana) {
            CanBePlayed = true;
            transform.FindChild("Cost").GetComponent<Text>().color = Color.black;
            int i;
            for (i = 0; i < aspects.Length; i++) {
                if (transform.parent.parent.GetComponent<PlayerStatus>().OwnAspects.Contains(aspects[i])) {
                    CanBePlayed = true;
                    transform.FindChild("Aspects").FindChild(aspects[i].ToString()).GetComponent<Text>().color = Color.black;
                } else {
                    CanBePlayed = false;
                    transform.FindChild("Aspects").FindChild(aspects[i].ToString()).GetComponent<Text>().color = Color.red;
                }
            }
            
        } else {
            CanBePlayed = false;
            transform.FindChild("Cost").GetComponent<Text>().color = Color.red;
            
        }

        if (Mathf.Abs(mouseDist[0]) <= 45 && Mathf.Abs(mouseDist[1]) <= 65.5f && Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().canPlay) {
            transform.position = new Vector3(transform.position.x, transform.parent.position.y + 10, transform.position.z);

            if (Input.GetMouseButtonDown(0) && CanBePlayed) {
                transform.parent.GetComponent<Hand>().DragCardStart(transform.gameObject);
            }
        } else
            transform.position = new Vector3(transform.position.x, transform.parent.position.y, transform.position.z);
    }
}
