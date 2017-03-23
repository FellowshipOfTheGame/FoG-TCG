using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyCard : MonoBehaviour{

    Transform playerHand;
    float[] mouseDist;
	

    void Start() {
        mouseDist = new float[2];
    }
	
	void Update () {
        //calcular distancia do mouse
        mouseDist[0] = Input.mousePosition.x - transform.position.x - 0.5f;
        mouseDist[1] = Input.mousePosition.y - transform.position.y - 2;

        // se o mouse estiver encima do botão
        if (Mathf.Abs(mouseDist[0]) <= 54.5f && Mathf.Abs(mouseDist[1]) <= 18.5f) {
            gameObject.GetComponent<Image>().color = Color.yellow;

            // clicando no botão
            if (Input.GetMouseButtonDown(0)) {
                //referencia a mão e o deck do jogador
                playerHand = transform.parent.parent.FindChild("Player"+Board.currPlayer).FindChild("Hand");

                playerHand.GetComponent<Hand>().PickUpCard();
            }
        } else
            gameObject.GetComponent<Image>().color = Color.white;
    }
 
}
