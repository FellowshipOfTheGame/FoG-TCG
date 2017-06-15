using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardAttack : MonoBehaviour {

    public int ATK;
    public int HP;
    public bool onTable = false;
    public bool canAttack = false;
    public Color cardColor;

    void Start() {
       cardColor = transform.GetComponent<Image>().color;
    }

    void Update() {

        if (onTable) {
            if (HP <= 0)
                RIP();

            if (canAttack)
                transform.GetComponent<Image>().color = new Color(cardColor.r, cardColor.g, cardColor.b);
            else
                transform.GetComponent<Image>().color = new Color(cardColor.r - 0.3f, cardColor.g - 0.3f, cardColor.b - 0.3f);
        }

    }

	public void Attack() {
        int posX = transform.parent.GetComponent<Slot>().pos[0];
        int posY = transform.parent.GetComponent<Slot>().pos[1];

        if (Board.currPlayer == 1 && posX<=1) {
            if (Board.cardMatriz[2, posY] != null) {
                GameObject target = Board.cardMatriz[2, posY];
                target.GetComponent<CardAttack>().HP -= ATK;
                target.transform.Find("HP").GetComponent<Text>().text = target.GetComponent<CardAttack>().HP.ToString();
                target.transform.Find("HP").GetComponent<Text>().color = Color.red;
            } else {
                if(posY >= Board.capt[1].GetComponent<CaptControl>().pos-1 && posY <= Board.capt[1].GetComponent<CaptControl>().pos + 1)
                    Board.player[1].GetComponent<PlayerStatus>().HP -= ATK;
            }
            canAttack = false;
        }
        if(Board.currPlayer == 2 && posX >= 2){
            if (Board.cardMatriz[1, posY] != null) {
                GameObject target = Board.cardMatriz[1, posY];
                target.GetComponent<CardAttack>().HP -= ATK;
                target.transform.Find("HP").GetComponent<Text>().text = target.GetComponent<CardAttack>().HP.ToString();
                target.transform.Find("HP").GetComponent<Text>().color = Color.red;
            } else {
                if (posY >= Board.capt[0].GetComponent<CaptControl>().pos - 1 && posY <= Board.capt[0].GetComponent<CaptControl>().pos + 1)
                    Board.player[0].GetComponent<PlayerStatus>().HP -= ATK;
            }
            canAttack = false;
        }
    }

    void RIP() {
        transform.parent.GetComponent<Slot>().IsFull = false;
        Board.cardMatriz[transform.parent.GetComponent<Slot>().pos[0], transform.parent.GetComponent<Slot>().pos[1]] = null;
        Destroy(transform.gameObject);
    }
}
