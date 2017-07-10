using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddCardInformation : MonoBehaviour {

    public CardInformation card;
    public Text title;
    public Image image;
    public Text desc;
    public Text flavor;
    public Text cost;
    public Text atk;
    public Text hp;
    public GameObject aspects;

	// Use this for initialization
    void Start() {
        name = card.title;
        title.text = card.title;
        desc.text = card.desc;
        flavor.text = card.flavor;
        cost.text = card.cost.ToString();
        atk.text = card.ATK.ToString();
        hp.text = card.HP.ToString();

        int i;
        for (i = 0; i < 4; i++)
            aspects.transform.GetChild(i).gameObject.SetActive(false);

		for(i=0;i<card.aspects.Count;i++)
            aspects.transform.Find(card.aspects[i].ToString()).gameObject.SetActive(true);

        switch (card.type) {
            case 'c':
                gameObject.AddComponent<CreatureCard>();
                gameObject.GetComponent<Image>().color = new Color(0.84f, 0.88f, 0.62f, gameObject.GetComponent<Image>().color.a);
                gameObject.AddComponent<CardAttack>();
                gameObject.GetComponent<CardAttack>().ATK = card.ATK;
                gameObject.GetComponent<CardAttack>().HP = card.HP;
                break;
            case 't':
                gameObject.AddComponent<TerrainCard>();
                gameObject.GetComponent<Image>().color = new Color(0.79f, 0.75f, 1.00f, gameObject.GetComponent<Image>().color.a);
                gameObject.AddComponent<CardResourceGenerator>();
                gameObject.GetComponent<CardResourceGenerator>().manaToGive = card.manaToGive;
				for (i = 0; i < card.aspectsToGive.Count; i++)
                    gameObject.GetComponent<CardResourceGenerator>().aspectsToGive.Add(card.aspectsToGive[i]);

                break;
            case 'a':
                gameObject.GetComponent<Image>().color = new Color(0.91f, 0.91f, 0.91f, gameObject.GetComponent<Image>().color.a);
                gameObject.AddComponent<CardResourceGenerator>();
				for (i = 0; i < card.aspectsToGive.Count; i++)
                    gameObject.GetComponent<CardResourceGenerator>().aspectsToGive.Add(card.aspectsToGive[i]);

                break;
        }

        if (GameManager.currScene == 'g')
            gameObject.AddComponent<CardInHand>();
	}


	public void click() {
		LoadCard.clickedCard = card;
		LoadCard.modificado = true;
	}
}
