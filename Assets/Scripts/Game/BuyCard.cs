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
        mouseDist[0] = Input.mousePosition.x - transform.position.x + 0.5f;
        mouseDist[1] = Input.mousePosition.y - transform.position.y - 2;

        // se o mouse estiver encima do botão
        if (Mathf.Abs(mouseDist[0]) <= 25.8f && Mathf.Abs(mouseDist[1]) <= 18.5f && Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().canBuy) {
            gameObject.GetComponent<Image>().color = Color.yellow;

            // clicando no botão
            if (Input.GetMouseButtonDown(0)) {
                //referencia a mão e o deck do jogador
                playerHand = transform.parent.parent.Find("Player" + Board.currPlayer).Find("Hand");

                playerHand.GetComponent<Hand>().PickUpCard();
            }
        } else {
            if (Board.player[Board.currPlayer - 1].GetComponent<PlayerStatus>().canBuy)
                gameObject.GetComponent<Image>().color = Color.white;
            else
                gameObject.GetComponent<Image>().color = new Color(1.0f, 0.5f, 0.5f);
        }
    }
 
}
