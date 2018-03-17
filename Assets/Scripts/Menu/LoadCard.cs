using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCard : MonoBehaviour {

	public Text dinheiro;

	public Text nome;
    public Text tipo;
    public Text custo;
    public Image image;
	public Text descricao;
    public Text flavor;
    public Text atk;
    public Text hp;
	public Text qtdd;
	public Text preco;
	//public Text nivel;
	//public Text preco_nivel;

	public Button comprar;
	//public Button upgrade;

	public static CardInformation clickedCard = null;
    public static Sprite chosenArt = null;
	public static bool modificado = true;
    public bool inGallery;

	void Update() {
		if (modificado) {
			modificado = false;
			LoadInfo();
		}
	}

	public void LoadInfo() {
		if(clickedCard != null) {
            nome.transform.parent.gameObject.SetActive(true);
            nome.text = clickedCard.title;
            custo.text = clickedCard.cost.ToString();
            atk.text = "--";
            hp.text = "--";
            switch (clickedCard.type) {
				case 'c':
					tipo.text = "Criatura";
                    atk.text = clickedCard.ATK.ToString();
                    hp.text = clickedCard.HP.ToString();
                    break;
				case 't':
                    tipo.text = "Terreno"; break;
				case 'a':
                    tipo.text = "Atmosfera"; break;
			}
            image.gameObject.SetActive(true);
            image.sprite = chosenArt;
      
			descricao.text = clickedCard.desc;
            flavor.text = clickedCard.flavor;

            if (!inGallery) {
                if (GameData.playerInfo.money >= clickedCard.price * (clickedCard.qtdd + 1) && clickedCard.qtdd < 3) {
                    comprar.interactable = true;
                    comprar.gameObject.SetActive(true);
                } else {
                    comprar.interactable = false;
                    comprar.gameObject.SetActive(false);
                }
                /*
                if (GameData.playerInfo.money >= clickedCard.lvl_price && clickedCard.lvl < 3) 
                    upgrade.interactable = true;
                else
                    upgrade.interactable = false;
                */
                dinheiro.text = "" + GameData.playerInfo.money;
                qtdd.text = "0" + clickedCard.qtdd.ToString();
                preco.text = (clickedCard.price * (clickedCard.qtdd + 1)).ToString();
                /*
                nivel.text = "0"+  clickedCard.lvl;
                preco_nivel.text = ""+  clickedCard.lvl_price;
                */
            }
		}
	}
}
