using UnityEngine;
using System.Collections.Generic;

// Server side player class
public class Player : MonoBehaviour {

    public static readonly uint HAND_MAX_SIZE = 10;
    public static readonly uint FIELD_SIZE = 5;
    public static readonly uint COMMANDER_SIZE = 3;

    public static readonly uint TERRAINS = 0;
    public static readonly uint CREATURES = 1;

    public List<Card> Hand;
    public List<Card> Deck;
    public Card[,] Field;
    public CommanderCard Commander;
    public uint CommanderPos;

    // TODO socket and stuff

    void Start() {
        Hand = new List<Card>();
        Field = new Card[2, FIELD_SIZE];
        CommanderPos = (FIELD_SIZE-COMMANDER_SIZE) / 2;
    }

    public bool DrawCard() {
        if (Deck.Count > 0) {
            Card Top = Deck[0];
            Deck.RemoveAt(0);
            if (Hand.Count < HAND_MAX_SIZE) {
                Hand.Add(Top);
            }
        }
        // TODO netcode stuff
    }

}
