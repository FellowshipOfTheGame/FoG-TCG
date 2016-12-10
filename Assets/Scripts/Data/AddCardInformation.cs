using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddCardInformation : MonoBehaviour {

    public CardInformation card;
    public Text title;
    public Image image;
    public Text desc;
    public Text flavor;
    public int quantity;

	// Use this for initialization
	void Start () {
        name = card.title;
        title.text = card.title;
        desc.text = card.desc;
        flavor.text = card.flavor;
	}
}
