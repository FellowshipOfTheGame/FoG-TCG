using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadDeck : MonoBehaviour {

	public Text nome;
	public InputField nomeI;
	public Text commander;
	public Text numCartas;

	public Button selecionar;
	public Button editar;
	public Button deletar;

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

		limpar ();

		if (nome != null) {
			nome.text = deck.name;
			if (deck.size < 10) numCartas.text = "0";
			else numCartas.text = "";
			numCartas.text +=  deck.size + "/30";

		} else {
			nomeI.text = deck.name;
			if (deck.size < 10) numCartas.text = "0";
			else numCartas.text = "";
			numCartas.text +=  deck.size + " / 30";
			CardList.GetComponent<DeckListManager> ().deckSize = deck.size;
		}
		commander.text = "" + deck.commander;



		int cartasCarregadas = 0;
		for (int i = 0; i < deck.size; i++) {
			GameObject aux = Instantiate (cardPrefab, CardList);

			int aux2=0;
			while (GameData.Cards [aux2++].title != deck.Cards[i].name && aux2 <= GameData.Cards.Count);

			aux.GetComponent<AddCardInformationMinimized> ().card = GameData.Cards [aux2-1];
			aux.GetComponent<AddCardInformationMinimized> ().quantity = deck.Cards[i].number;

			DeckListDraggable drag = aux.GetComponent<DeckListDraggable> ();
			if (drag != null) {
				drag.deckListZone = CardList.gameObject;
				drag.canvas = CardList.GetComponentInParent<Canvas>();
			}

			if (deck.Cards [i].number > 1) {
				cartasCarregadas += deck.Cards [i].number;
			} else
				cartasCarregadas++;

			if (cartasCarregadas >= deck.size)
				break;
		}

		if (selecionar != null) {
			selecionar.interactable = true;
			editar.interactable = true;
			deletar.interactable = true;
		}
	}

	public void limpar() {

		if (nome != null) nome.text = "Nome...";
		else nomeI.text = "";

		commander.text = "";
		numCartas.text =  "00 / 30";

		if (selecionar != null) {
			selecionar.interactable = false;
			editar.interactable = false;
			deletar.interactable = false;
		}
		//esvazia a lista de cartas
		foreach (Transform child in CardList.transform) {
			GameObject.Destroy(child.gameObject);
		}

	}

}
