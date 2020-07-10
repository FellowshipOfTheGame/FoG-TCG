using UnityEngine;

// TODO tirar esse estado? binário já implementou como colocar uma carta em campo
public class SelectOptionState : GameState {
	/* Tirar o warning
    private Card c;

    public override void Enter() {
        base.Enter();
        AddListeners();
        c = args[0] as Card;

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
        bm.ButtonPressedEvent += ButtonPressed;
    }

    void RemoveListeners() {
        bm.ButtonPressedEvent -= ButtonPressed;
    }

    void ButtonPressed(GameObject obj) {
        // TODO detect which button it was
        // TODO stuff
    }
*/
}
