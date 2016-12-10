
public sealed class SelectTargetState : GameState {

    protected void AddListeners() {
        board.CardSelectedEvent += SelectCard;
    }

    protected void RemoveListeners() {
        board.CardSelectedEvent -= SelectCard;
    }

}
