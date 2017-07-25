using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public List<string> deckList;
    public GameObject genericCard;

    // Use this for initialization
    void Start () {
		
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
        }
    }
}
