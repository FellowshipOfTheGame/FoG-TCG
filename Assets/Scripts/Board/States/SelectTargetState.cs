
public class SelectTargetState : GameState {

    public void Enter() {
        base.Enter();
        AddListeners();
    }

    public void Exit() {
        RemoveListeners();
        base.Exit();
    }

    void AddListeners() {
        board.CardSelectedEvent += SelectCard;
    }

    void RemoveListeners() {
        board.CardSelectedEvent -= SelectCard;
    }

    void SelectCard(Card c) {
        // TODO logic
    }

}
