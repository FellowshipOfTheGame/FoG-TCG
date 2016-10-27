using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddCardInformationMinimized : MonoBehaviour {

    public CardInformation card;
    public Text title;
    public Text quantity;

    // Use this for initialization
    void Start()
    {
        title.text = card.title;
    }
}
