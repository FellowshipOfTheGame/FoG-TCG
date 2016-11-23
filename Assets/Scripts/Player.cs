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

    public TerrainCard[] Terrains;
    public CreatureCard[] Creatures;

    public Card[][] Field {
        get {
            return new Card[][] {
                Terrains,
                Creatures
            };
        }
    }

    public CommanderCard Commander;
    public uint CommanderPos;

    private Card selectedCard;
    public Card SelectedCard {
        get {
            return selectedCard;
        }
        set {
            selectedCard = value;
            // TODO notify Board / GameManager
        }
    }

    // TODO socket and stuff

    void Start() {
        Hand = new List<Card>();
        Terrains = new TerrainCard[FIELD_SIZE];
        Creatures = new CreatureCard[FIELD_SIZE];
        CommanderPos = (FIELD_SIZE-COMMANDER_SIZE) / 2;
    }

    public void DrawCard() {
        if (Deck.Count > 0) {
            Card Top = Deck[0];
            Deck.RemoveAt(0);
            GiveCard(Top);
        }
        // TODO netcode stuff
    }

    public void GiveCard(Card c) {
        if (Hand.Count < HAND_MAX_SIZE) {
            Hand.Add(c);
        }
        // TODO netcode stuff
    }

}
