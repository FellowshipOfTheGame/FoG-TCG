
public class SelectTargetState : GameState {

    public void Enter() {
        base.Enter();
        AddListeners();
    }

    public void Exit() {
        RemoveListeners();
        base.Exit();
    }

    protected void AddListeners() {
        board.CardSelectedEvent += SelectCard;
    }

    protected void RemoveListeners() {
        board.CardSelectedEvent -= SelectCard;
    }

    void SelectCard(Card c) {
        // TODO logic
    }

}
