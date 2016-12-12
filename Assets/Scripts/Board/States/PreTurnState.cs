
public class PreTurnState : GameState {

    public override void Enter() {
        foreach (Card[] row in board.currentPlayer.field)
            foreach (Card c in row)
                c.OnTurnStart();
        // TODO process events queued by delegates
        SetState<OverviewState>();
    }

    public override void Exit() {
        // TODO enable GUI elements for current player
    }

}
