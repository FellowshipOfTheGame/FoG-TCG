using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Deck {

    public static Deck current;
    public static string DeckName;
    public static int DeckSize;
	public static List<CardInformation> Cards = new List<CardInformation>();
}
