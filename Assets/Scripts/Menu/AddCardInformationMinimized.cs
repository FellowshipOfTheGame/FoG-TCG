using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddCardInformationMinimized : MonoBehaviour {

    public CardInformation card;
    public Text title;
    public Text quantityText;
    public int quantity = 0;

    // Use this for initialization
    void Start() {
        name = card.title; 
        title.text = card.title;
		quantityText.text = "x"+ quantity;
    }
}
