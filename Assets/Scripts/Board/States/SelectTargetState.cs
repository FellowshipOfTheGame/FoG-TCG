
public class SelectTargetState : GameState {

    private Predicate<GameObject> validSelection;

    public override void Enter() {
        base.Enter();
        AddListeners();
        validSelection = arg[0] as Predicate<GameObject>;
    }

    public override void Exit() {
        RemoveListeners();
        base.Exit();
    }

    void AddListeners() {
        owner.CardSelectedEvent += SelectCard;
        owner.ActionCanceledEvent += Return;
    }

    void RemoveListeners() {
        owner.ActionCanceledEvent -= Return;
        owner.CardSelectedEvent -= SelectCard;
    }

    void SelectCard(GameObject obj) {
        if (validSelection(obj)) {
            // TODO call OnCardPlayed somehow
            // TODO get position in board from obj
            SetState<OverviewState>();
        }
    }

}
