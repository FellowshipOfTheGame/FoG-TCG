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
    public GameObject[] aspects;
    
    public Image frame;
    public Color terrain, atm, spell;

	// Use this for initialization
    void Start() {
        name = card.title;
            title.text = card.title;
            desc.text = card.desc;
            flavor.text = card.flavor;
            cost.text = card.cost.ToString();
        if (card.type == 'c') {
            atk.transform.parent.gameObject.SetActive(true);
            atk.text = card.ATK.ToString();
            hp.text = card.HP.ToString();
        }else{
            atk.transform.parent.gameObject.SetActive(false);
            if(card.type == 't') frame.color = terrain;
            else if(card.type == 'a') frame.color = atm;
            else if(card.type == 's') frame.color = spell;
        }

		int aux2=0;
		while (GameData.Images [aux2++].card != card.title && aux2 <= GameData.Images.Count);
		if(GameData.Images[aux2-1].imagem != null)
			image.sprite = GameData.Images[aux2-1].imagem;
		else image.sprite = GameData.Images[GameData.Images.Count-1].imagem;

            int i, j;
            for (i = 0; i < 4; i++)
                aspects[0].transform.GetChild(i).gameObject.SetActive(false);
            
            int cont = 0;
        for (i = 0; i < 4; i++) {
            for (j = 0; j < card.aspects[i]; j++) {
                cont++;
                GameObject temp;
                if (cont <= 4)
                    temp = Instantiate(aspects[0].transform.GetChild(i).gameObject, aspects[0].transform);
                else
                    temp = Instantiate(aspects[0].transform.GetChild(i).gameObject, aspects[1].transform);
                temp.SetActive(true);
            }
        }
	}


	public void click() {
		LoadCard.clickedCard = card;
        LoadCard.chosenArt = image.sprite;
		LoadCard.modificado = true;
	}
}
