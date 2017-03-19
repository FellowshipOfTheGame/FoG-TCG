using UnityEngine;

public class BoardManager : StateMachine {
    public delegate void ToggleTurnDelegate();
    public delegate void ButtonPressedDelegate(GameObject obj);
    public delegate void CardSelectedDelegate(Card card);
    public delegate void CardMouseEnterDelegate(Card card);
    public delegate void CardMouseExitDelegate(Card card);
    public delegate void ActionCanceledDelegate();

    public event ToggleTurnDelegate ToggleTurnEvent;
    public event ButtonPressedDelegate ButtonPressedEvent;
    public event CardSelectedDelegate CardSelectedEvent;
    public event CardMouseEnterDelegate CardMouseEnterEvent;
    public event CardMouseExitDelegate CardMouseExitEvent;
    public event ActionCanceledDelegate ActionCanceledEvent;

    public Board board;
    public int turn;

    void Awake() {
        board = transform.parent.GetComponent<Board>();
        turn = 0;
    }

    public void EndTurn() {
        SetState<PostTurnState>();
    }

    public void OnToggleTurn() {
        var handler = this.ToggleTurnEvent;
        if (handler != null)
            handler();
    }

    public void OnButtonPressed(GameObject obj) {
        var handler = this.ButtonPressedEvent;
        if (handler != null)
            handler(obj);
    }

    public void OnCardSelected(Card card) {
        var handler = this.CardSelectedEvent;
        if (handler != null)
            handler(card);
    }

    public void OnCardMouseEnter(Card card) {
        var handler = this.CardMouseEnterEvent;
        if (handler != null)
            handler(card);
    }

    public void OnCardMouseExit(Card card) {
        var handler = this.CardMouseExitEvent;
        if (handler != null)
            handler(card);
    }
}
