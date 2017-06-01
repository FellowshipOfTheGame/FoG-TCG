using UnityEngine;
using System.Collections.Generic;

// Server side player class
public class Player : MonoBehaviour {

    public static readonly uint HAND_MAX_SIZE = 10;
    public static readonly uint FIELD_SIZE = 5;
    public static readonly uint COMMANDER_SIZE = 3;

    public static readonly uint TERRAINS = 0;
    public static readonly uint CREATURES = 1;

    public Board board;

    public List<Card> hand;
    public List<Card> deck;

    public TerrainCard[] terrains;
    public CreatureCard[] creatures;

    public Card[][] field {
        get {
            return new Card[][] {
                terrains,
                creatures
            };
        }
    }

    public CommanderCard commander;
    public uint commanderPos;

    // TODO socket and stuff

    void Start() {
        hand = new List<Card>();
        terrains = new TerrainCard[FIELD_SIZE];
        creatures = new CreatureCard[FIELD_SIZE];
        commanderPos = (FIELD_SIZE-COMMANDER_SIZE) / 2;
        board = GameObject.FindObjectOfType(typeof(Board)) as Board;
    }

    public void DrawCard() {
        if (deck.Count > 0) {
            Card top = deck[0];
            deck.RemoveAt(0);
            GiveCard(top);
        }
        // TODO netcode stuff
    }

    public void GiveCard(Card c) {
        if (hand.Count < HAND_MAX_SIZE) {
            hand.Add(c);
        }
        // TODO netcode stuff
    }

}
