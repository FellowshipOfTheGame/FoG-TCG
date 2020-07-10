using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddDeckInformation : MonoBehaviour {

	public DeckInformation deck;

	public Text nome;
	public Image comandante;
	public Text vitorias;
	public Text criacao;

	void Start () {
		nome.text = deck.name;
		vitorias.text = "" + deck.victories;
		criacao.text =  "" + deck.criationDate;
		Debug.Log(this.GetComponent<RectTransform>().sizeDelta);

	}

	public void click() {
		LoadDeck.clickedDeck = deck.name;
		LoadDeck.modificado = true;
	}

}
