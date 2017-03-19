using System;
using UnityEngine;

public class SelectTargetState : GameState {

    private Predicate<Card> validSelection;

    public override void Enter() {
        base.Enter();
        AddListeners();
        validSelection = args[0] as Predicate<Card>;
    }

    public override void Exit() {
        RemoveListeners();
        base.Exit();
    }

    void AddListeners() {
        bm.CardSelectedEvent += SelectCard;
        bm.ActionCanceledEvent += Return;
    }

    void RemoveListeners() {
        bm.ActionCanceledEvent -= Return;
        bm.CardSelectedEvent -= SelectCard;
    }

    void SelectCard(Card obj) {
        if (validSelection(obj)) {
            // TODO call OnCardPlayed somehow
            // TODO get position in board from obj
            SetState<OverviewState>();
        }
    }

}
