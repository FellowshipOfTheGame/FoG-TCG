
public class PostTurnState : GameState {

    public override void Enter() {
		/*
		TODO fix board attributes
        foreach (Card[] row in board.currentPlayer.field)
            foreach (Card c in row)
                c.OnTurnEnd();
        // TODO process events queued by delegates
        bm.turn = ~bm.turn & 1;
        // TODO wait for any end of turn type animations or sprites
        SetState<PreTurnState>();
        */
    }

    public override void Exit() {
        // TODO disable GUI elements for the player who just finished
        // NOTE the player who just played is board.OppositePlayer by this point
        // in execution
    }

}
