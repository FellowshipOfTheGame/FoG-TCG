using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    public List<CardInformation> deckList;

}

public class DeckInformation {

	public string name;
	public string commander;
	public int size;
	public string criationDate;
	public int victories;

	public List<numCards> Cards = new List<numCards>();

}

[System.Serializable]
public class numCards {

	[SerializeField]
	public string name;

	[SerializeField]
	public int number;

	public numCards(string s, int n) {
		name = s;
		number = n;
	}
}
