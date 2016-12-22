
public class SelectOptionState : GameState {

    private Card c;

    public override void Enter() {
        base.Enter();
        AddListeners();
        c = arg[0] as Card;

        //if (c in hand)
        //    SetState<SelectTarget>(new object[] {c.targetChecker});
        //      targetChecker and playAction are card-specific delegates
        //      targetChecker checks if the card can be played from the hand
        //          into a position
    }

    public override void Exit() {
        RemoveListeners();
        base.Exit();
    }

    void AddListeners() {
        owner.ButtonPressedEvent += ButtonPressed;
    }

    void RemoveListeners() {
        owner.ButtonPressedEvent -= ButtonPressed;
    }

    void ButtonPressed(GameObject obj) {
        // TODO detect which button it was
        // TODO stuff
    }

}
