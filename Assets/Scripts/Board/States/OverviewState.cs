
public class OverviewState : GameState {
    
    public override void Enter() {
        base.Enter();
        AddListeners();
        // TODO enable buttons & stuff
    }

    public override void Exit() {
        // TODO disable buttons & stuff (maybe?)
        RemoveListeners();
        base.Exit();
    }

    void AddListeners() {
        board.CardSelectedEvent += SelectCard;
        board.CardHoveredEvent += HoverCard;
    }

    void RemoveListeners() {
        board.CardSelectedEvent -= SelectCard;
        board.CardHoveredEvent -= HoverCard;
    }

    void HoverCard(Card c) {
        // TODO show info
    }

    void SelectCard(Card c) {
        // TODO
        // if (c in hand)
        //     SetState<PlayCardState>();
        // else if (c in board.CurrentPlayer.Field)
        //     SetState<ActivateCardState>();
        // else
        //     ??? (player clicked in opposite player card)
    }

}
