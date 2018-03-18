using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

[MoonSharpUserData]
public class Player : MonoBehaviour {

    public List<string> originalDeck;
    public List<string> deckList;
    public int index;
    public int mana;
    public int HP;
    public int[] aspects;
    public Captain capt;
    [Space(5)]
    public GameObject genericCard;
    public float cardDist;
    public float cardWeight;
    [HideInInspector]
    public bool canBuy = true;
    public bool canPlay = true;

    public Board board;
    // Use this for initialization
    void Start () {
        aspects = new int[4];
        int i;
        for (i = 0; i < 4; i++)
            aspects[i] = 0;
	}
	
    public void ResetTurn() {
        canBuy = true;
        canPlay = true;
    }

    public void PickUpCard() { //load a random card from decklist
        if (this.transform.childCount <= 7 && deckList.Count > 0) {
            int index = Random.Range(0, deckList.Count - 1);
            GameObject newCard = Instantiate(genericCard, this.transform);
            newCard.GetComponent<Card>().board = this.transform.parent.GetComponent<Board>();
            newCard.GetComponent<AddCardInformationSemCanvas>().info = newCard.GetComponent<Card>();
            newCard.GetComponent<Card>().LoadScript(deckList[index]);
            deckList.RemoveAt(index);
            newCard = null;
            RefreshChildPositon();
        }
    }

    public void RefreshChildPositon() {
        Vector3 myPos = this.transform.position;
        float initialShift;
        if (this.transform.childCount % 2 == 0) {
            initialShift = (cardDist + cardWeight) / 2;
            int i, j = 0;
            for(i = this.transform.childCount / 2 - 1; i >= 0; i--) {
                this.transform.GetChild(i).position = new Vector3(myPos.x - initialShift - j * (cardDist + cardWeight), myPos.y, myPos.z);
                j++;
            }
            j = 0;
            for (i = this.transform.childCount / 2; i <= this.transform.childCount - 1; i++) {
                this.transform.GetChild(i).position = new Vector3(myPos.x + initialShift + j * (cardDist + cardWeight), myPos.y, myPos.z);
                j++;
            }
        }else {
            initialShift = cardWeight + cardDist;
            this.transform.GetChild((this.transform.childCount - 1) / 2).position = new Vector3(myPos.x, myPos.y, myPos.z);
            int i, j = 0;
            for (i = (this.transform.childCount - 1) / 2 - 1; i >= 0; i--) {
                this.transform.GetChild(i).position = new Vector3(myPos.x - initialShift - j * (cardDist + cardWeight), myPos.y, myPos.z);
                j++;
            }
            j = 0;
            for (i = (this.transform.childCount - 1) / 2 + 1; i <= this.transform.childCount - 1; i++) {
                this.transform.GetChild(i).position = new Vector3(myPos.x + initialShift + j * (cardDist + cardWeight), myPos.y, myPos.z);
                j++;
            }
        }
    }

    public void Damage(Table args){
        int prevHP = HP;
        HP -= args.Get(4).ToObject<int>();
        print(HP);

        if (prevHP > board.critic && HP <= board.critic)
            board.changeMusic(true);

        if (HP <= 0) {
            Board.winner = 3 - index;
            board.EndGame();
        }
    }
}
