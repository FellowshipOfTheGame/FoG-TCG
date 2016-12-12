
public class SelectTargetState : GameState {

    public override void Enter() {
        base.Enter();
        AddListeners();
    }

    public override void Exit() {
        RemoveListeners();
        base.Exit();
    }

    void AddListeners() {
        owner.CardSelectedEvent += SelectCard;
    }

    void RemoveListeners() {
        owner.CardSelectedEvent -= SelectCard;
    }

    void SelectCard(Card c) {
        // TODO logic
    }

}
