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
    void Start() {
        name = card.title;
        title.text = card.title;
        desc.text = card.desc;
        flavor.text = card.flavor;

        switch (card.type) {
            case 'c':
                gameObject.AddComponent<CreatureCard>();
                gameObject.GetComponent<Image>().color = new Color(0.84f,0.88f,0.62f);
                break;
            case 't':
                gameObject.AddComponent<TerrainCard>();
                gameObject.GetComponent<Image>().color = new Color(0.79f, 0.75f, 1.00f);
                break;
            default:
                break;
        }
	}

    
}
