using UnityEngine;
using System.Collections.Generic;

// Server side player class
public class Player : MonoBehaviour {

    public static readonly uint HAND_MAX_SIZE = 10;

    public List<Card> hand;
    public List<Card> deck;
    // TODO socket and stuff

    void Start() {
        hand = new List<Card>();
    }

    public bool DrawCard() {
        if (deck.Count > 0) {
            Card top = deck[0];
            deck.RemoveAt(0);
            if (hand.Count < HAND_MAX_SIZE) {
                hand.Add(top);
            }
        }
        // TODO netcode stuff
    }

}
