
public class PostTurnState : GameState {

    public override void Enter() {
        foreach (Card[] row in board.CurrentPlayer.Field)
            foreach (Card c in row)
                c.OnTurnEnd();
        // TODO process events queued by delegates
        owner.turn = ~owner & 1;
        // TODO wait for any end of turn type animations or sprites
        SetState<PreTurnState>();
    }

    public override void Exit() {
        // TODO disable GUI elements for the player who just finished
        // NOTE the player who just played is board.OppositePlayer by this point
        // in execution
    }

}
