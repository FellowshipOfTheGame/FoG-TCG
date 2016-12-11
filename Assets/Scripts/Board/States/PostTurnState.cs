
public class PostTurnState : GameState {

    public void Enter() {
        foreach (Card[] row in board.CurrentPlayer.Field)
            foreach (Card c in row)
                c.OnTurnEnd();
        // TODO process events queued by delegates
        owner.turn = ~owner & 1;
        // TODO wait for any end of turn type animations or sprites
        owner.SetState<PreTurnState>();
    }

}
