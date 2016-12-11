
public class PreTurnState : GameState {

    public void Enter() {
        foreach (Card[] row in board.CurrentPlayer.Field)
            foreach (Card c in row)
                c.OnTurnStart();
        // TODO process events queued by delegates
        owner.SetState<OverviewState>();
    }

}
