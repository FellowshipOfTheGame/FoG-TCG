using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCard : MonoBehaviour {

	public Text dinheiro;

	public Text infos;
	public Text descricao;

	public Text qtdd;
	public Text preco;
	public Text nivel;
	public Text preco_nivel;

	public Button comprar;
	public Button upgrade;

	public static CardInformation clickedCard = null;
	public static bool modificado = true;

	void Update() {
		if (modificado) {
			modificado = false;
			LoadInfo();
		}
	}

	public void LoadInfo() {
		dinheiro.text = ""+GameData.playerInfo.money;

		if(clickedCard != null) {
			if (GameData.playerInfo.money >= clickedCard.price*(clickedCard.qtdd+1) && clickedCard.qtdd < 3) 
				comprar.interactable = true;
			else
				comprar.interactable = false;

			if (GameData.playerInfo.money >= clickedCard.lvl_price && clickedCard.lvl < 3) 
				upgrade.interactable = true;
			else
				upgrade.interactable = false;
			

			infos.text = "Nome: " + clickedCard.title;

			switch (clickedCard.type) {
				case 'c':
					infos.text += "\nTipo: Criatura"; break;
				case 't':
					infos.text += "\nTipo: Terreno"; break;
				case 'a':
					infos.text += "\nTipo: Atmosfera"; break;
			}

			infos.text += "\nDescrição: ";
			descricao.text = "\t" + clickedCard.desc;

			qtdd.text = "0"+ clickedCard.qtdd;
			preco.text = ""+  clickedCard.price * (clickedCard.qtdd+1);
			nivel.text = "0"+  clickedCard.lvl;
			preco_nivel.text = ""+  clickedCard.lvl_price;
		
		}
	}
}
