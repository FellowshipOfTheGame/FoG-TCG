using UnityEngine;

public class Board : MonoBehaviour {

    public delegate void ToggleTurnDelegate();
    public event ToggleTurnDelegate ToggleTurnEvent;

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
        get { return P[(Turn+1) & 1]; }
    }

    public int Turn;

    public int ToggleTurn() {
        foreach (Card[] row in CurrentPlayer.Field)
            foreach (Card c in row)
                c.OnTurnEnd();
        Turn = (Turn+1) & 1;

        OnToggleTurn();
        foreach (Card[] row in CurrentPlayer.Field)
            foreach (Card c in row)
                c.OnTurnStart();

        return Turn;
    }

    public void OnToggleTurn() {
        var handler = this.ToggleTurnEvent;
        if (handler != null)
            handler();
    }

}
