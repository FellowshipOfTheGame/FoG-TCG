using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour {

    float[] mouseDist;


    void Start() {
        mouseDist = new float[2];
    }

    void Update() {
        //calcular distancia do mouse
        mouseDist[0] = Input.mousePosition.x - transform.position.x - 0.5f;
        mouseDist[1] = Input.mousePosition.y - transform.position.y - 2;

        // se o mouse estiver encima do botão
        if (Mathf.Abs(mouseDist[0]) <= 54.5f && Mathf.Abs(mouseDist[1]) <= 18.5f) {
            gameObject.GetComponent<Image>().color = Color.yellow;

            // clicando no botão
            if (Input.GetMouseButtonDown(0)) {
                Board.TurnChange();
            }
        } else
            gameObject.GetComponent<Image>().color = Color.white;
    }
}