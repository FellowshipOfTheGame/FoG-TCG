using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUpgradeCard : MonoBehaviour {

	public GameManager gm;

	public void BuyCard() {
		Debug.Log (""+LoadCard.clickedCard.price);
		if (GameData.playerInfo.money >= LoadCard.clickedCard.price*(LoadCard.clickedCard.qtdd+1) && LoadCard.clickedCard.qtdd < 7) {
			GameData.playerInfo.money -= LoadCard.clickedCard.price*(LoadCard.clickedCard.qtdd+1);
			LoadCard.clickedCard.qtdd++;
		}

		returnCard ();
	}

	public void UpgradeCard() {
		if (GameData.playerInfo.money >= LoadCard.clickedCard.lvl_price && LoadCard.clickedCard.lvl < 3) {
			GameData.playerInfo.money -= LoadCard.clickedCard.lvl_price;
			LoadCard.clickedCard.lvl++;
		}

		returnCard ();
	}

	private void returnCard() {
		int index = 0;
		while (GameData.Cards [index++].title != LoadCard.clickedCard.title);

		GameData.Cards [index-1] = LoadCard.clickedCard;

		LoadCard.modificado = true;

		gm.SaveCard (LoadCard.clickedCard);
		gm.SaveInfos ();

	}

}
