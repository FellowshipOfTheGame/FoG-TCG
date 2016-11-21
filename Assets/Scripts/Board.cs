using UnityEngine;

public class Board : MonoBehaviour {
    private Player[] P;
    public Player P1 {
        get { return P[0]; }
    }
    public Player P2 {
        get { return P[1]; }
    }
    
    public Player CurrentPlayer {
        get { return P[Turn]; }
    }
    public Player OppositePlayer {
        get { return P[(Turn+1) % 2]; }
    }

    public int Turn;

    public int ToggleTurn() {
        Turn = (Turn+1) % 2
    }
}
