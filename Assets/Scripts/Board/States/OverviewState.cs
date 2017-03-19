using UnityEngine;

public class OverviewState : GameState {
    
    public override void Enter() {
        base.Enter();
        AddListeners();
        bm.ClearHistory();
        // TODO enable buttons & stuff
    }

    public override void Exit() {
        // TODO disable buttons & stuff (maybe?)
        // otherwise other states should turn off whatever they don't want
        RemoveListeners();
        base.Exit();
    }

    void AddListeners() {
        bm.CardSelectedEvent += SelectCard;
        bm.CardMouseEnterEvent += CardMouseEnter;
        bm.CardMouseExitEvent += CardMouseExit;
        bm.ButtonPressedEvent += ButtonPressed;
    }

    void RemoveListeners() {
        bm.ButtonPressedEvent -= ButtonPressed;
        bm.CardMouseEnterEvent -= CardMouseEnter;
        bm.CardMouseExitEvent -= CardMouseExit;
        bm.CardSelectedEvent -= SelectCard;
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

    void ButtonPressed(GameObject obj) {
        if (obj == GuiManager.BtnEndTurn)
            SetState<PostTurnState>();
    }

}
