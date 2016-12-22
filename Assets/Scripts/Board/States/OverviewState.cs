
public class OverviewState : GameState {
    
    public override void Enter() {
        base.Enter();
        AddListeners();
        owner.ClearHistory();
        // TODO enable buttons & stuff
    }

    public override void Exit() {
        // TODO disable buttons & stuff (maybe?)
        // otherwise other states should turn off whatever they don't want
        RemoveListeners();
        base.Exit();
    }

    void AddListeners() {
        owner.CardSelectedEvent += SelectCard;
        owner.CardMouseEnterEvent += CardMouseEnter;
        owner.CardMouseExitEvent += CardMouseExit;
        owner.ButtonPressedEvent += ButtonPressed;
    }

    void RemoveListeners() {
        owner.ButtonPressedEvent -= ButtonPressed;
        owner.CardMouseEnterEvent -= CardMouseEnter;
        owner.CardMouseExitEvent -= CardMouseExit;
        owner.CardSelectedEvent -= SelectCard;
    }

    void CardMouseEnter(Card c) {
        // TODO hide prev card info
        // TODO show info
    }

    void CardMouseExit(Card c) {
        // TODO hide info
    }

    void SelectCard(Card c) {
        SetState<SelectOptionState>(new object[] {c});
    }

    void ButtonPressedEvent(GameObject obj) {
        if (obj == GuiManager.BtnEndTurn)
            SetState<PostTurnState>();
    }

}
