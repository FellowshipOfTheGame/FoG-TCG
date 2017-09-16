﻿using UnityEngine;
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

		int aux2=0;
		while (GameData.Images [aux2++].card != card.title && aux2 <= GameData.Images.Count);
		if(GameData.Images[aux2-1].imagem != null)
			image.sprite = GameData.Images[aux2-1].imagem;
		else image.sprite = GameData.Images[GameData.Images.Count-1].imagem;

        int i;
        for (i = 0; i < 4; i++)
            aspects.transform.GetChild(i).gameObject.SetActive(false);

		for(i=0;i<card.aspects.Count;i++)
            aspects.transform.Find(card.aspects[i].ToString()).gameObject.SetActive(true);

        switch (card.type) {
            case 'c':
                gameObject.AddComponent<CreatureCard>();
                gameObject.GetComponent<Image>().color = new Color(0.84f, 0.88f, 0.62f, gameObject.GetComponent<Image>().color.a);
                break;
            case 't':
                gameObject.AddComponent<TerrainCard>();
                gameObject.GetComponent<Image>().color = new Color(0.79f, 0.75f, 1.00f, gameObject.GetComponent<Image>().color.a);

                break;
            case 'a':
                gameObject.GetComponent<Image>().color = new Color(0.91f, 0.91f, 0.91f, gameObject.GetComponent<Image>().color.a);
                break;
        }

	}


	public void click() {
		LoadCard.clickedCard = card;
		LoadCard.modificado = true;
	}
}
