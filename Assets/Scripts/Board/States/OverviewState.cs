
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
        owner.CardSelectedEvent += SelectCard;
        owner.CardMouseEnterEvent += CardMouseEnter;
        owner.CardMouseExitEvent += CardMouseExit;
    }

    void RemoveListeners() {
        owner.CardMouseEnterEvent -= CardMouseEnter;
        owner.CardMouseExitEvent -= CardMouseExit;
        owner.CardSelectedEvent -= SelectCard;
    }

    void CardMouseEnter(Card c) {
        // TODO show info
        // TODO hide prev card info
    }

    void CardMouseExit(Card c) {
        // TODO hide info
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
