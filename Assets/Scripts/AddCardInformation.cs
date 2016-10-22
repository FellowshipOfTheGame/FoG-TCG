using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddCardInformation : MonoBehaviour {

    public CardInformation card;
    public Text title;
    public Image image;
    public Text desc;
    public Text flavor;

	// Use this for initialization
	void Start () {
        title.text = card.title;
        desc.text = card.desc;
        flavor.text = card.flavor;
	}
}
