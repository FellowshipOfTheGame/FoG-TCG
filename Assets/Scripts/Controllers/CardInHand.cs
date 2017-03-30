using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInHand : MonoBehaviour {

    float[] mouseDist;

	// Use this for initialization
	void Start () {
        mouseDist = new float[2];
	}

    // Update is called once per frame
    void Update() {
        mouseDist[0] = Input.mousePosition.x - transform.position.x;
        mouseDist[1] = Input.mousePosition.y - transform.position.y;

        if (Mathf.Abs(mouseDist[0]) <= 45 && Mathf.Abs(mouseDist[1]) <= 65.5f && Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().canPlay) {
            transform.position = new Vector3(transform.position.x, transform.parent.position.y + 10, transform.position.z);

            if (Input.GetMouseButtonDown(0)) {
                transform.parent.GetComponent<Hand>().DragCardStart(transform.gameObject);
            }
        } else
            transform.position = new Vector3(transform.position.x, transform.parent.position.y, transform.position.z);
    }
}
