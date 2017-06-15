using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadDeck : MonoBehaviour {
	
	public Text nome;
	public Text commander;
	public Text numCartas;

	public DeckInformation deck;

	public Transform CardList;
	public GameObject cardPrefab;

	public static string clickedDeck;
	public static bool modificado = false;

	void Update() {
		if (modificado) {
			modificado = false;
			LoadInfo();
		}
	}

	public void LoadInfo() {
		int index = 0;
		while (GameData.Decks [index++].name != clickedDeck);

		deck = GameData.Decks [index-1];

		nome.text = deck.name;
		commander.text = "" + deck.commander;
		numCartas.text =  "" + deck.size;
	
		//esvazia a lista de cartas
		foreach (Transform child in CardList.transform) {
			GameObject.Destroy(child.gameObject);
		}
		
		int cartasCarregadas = 0;
		for (int i = 0; i < deck.size; i++) {
			GameObject aux = Instantiate (cardPrefab, CardList);

			int aux2=0;
			while (GameData.Cards [aux2++].title != deck.Cards[i].name && aux2 <= GameData.Cards.Count);

			aux.GetComponent<AddCardInformationMinimized> ().card = GameData.Cards [aux2-1];
			aux.GetComponent<AddCardInformationMinimized> ().quantity = deck.Cards[i].number;


			if (deck.Cards [i].number > 1) {
				cartasCarregadas += deck.Cards [i].number;
			} else
				cartasCarregadas++;

			if (cartasCarregadas >= deck.size)
				break;
		}	
	}
}
