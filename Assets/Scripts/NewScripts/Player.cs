using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public List<string> deckList;
    public int mana;
    public int[] aspects;

    public GameObject genericCard;
    public float cardDist;
    public float cardWeight;

    // Use this for initialization
    void Start () {
        aspects = new int[4];
        int i;
        for (i = 0; i < 4; i++)
            aspects[i] = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PickUpCard() { //load a random card from decklist
        if (this.transform.childCount <= 7 && deckList.Count > 0) {
            int index = Random.Range(0, deckList.Count - 1);
        
            GameObject newCard = Instantiate(genericCard, this.transform);
            newCard.GetComponent<Card>().board = this.transform.parent.GetComponent<Board>();
            newCard.GetComponent<AddCardInformation>().info = newCard.GetComponent<Card>();
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
            for(i= this.transform.childCount / 2 - 1; i >= 0; i--) {
                this.transform.GetChild(i).position = new Vector3(myPos.x - initialShift - j * (cardDist + cardWeight), myPos.y, -3.55f);
                j++;
            }
            j = 0;
            for (i = this.transform.childCount / 2; i <= this.transform.childCount - 1; i++) {
                this.transform.GetChild(i).position = new Vector3(myPos.x + initialShift + j * (cardDist + cardWeight), myPos.y, -3.55f);
                j++;
            }
        }else {
            initialShift = cardWeight + cardDist;
            this.transform.GetChild((this.transform.childCount - 1) / 2).position = new Vector3(myPos.x, myPos.y, -3.55f);
            int i, j = 0;
            for (i = (this.transform.childCount - 1) / 2 - 1; i >= 0; i--) {
                this.transform.GetChild(i).position = new Vector3(myPos.x - initialShift - j * (cardDist + cardWeight), myPos.y, -3.55f);
                j++;
            }
            j = 0;
            for (i = (this.transform.childCount - 1) / 2 + 1; i <= this.transform.childCount - 1; i++) {
                this.transform.GetChild(i).position = new Vector3(myPos.x + initialShift + j * (cardDist + cardWeight), myPos.y, -3.55f);
                j++;
            }
        }
    }

}
