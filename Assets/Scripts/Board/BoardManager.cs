
public class BoardManager : StateMachine {
    public delegate void ToggleTurnDelegate();
    public delegate void CardSelectedDelegate(Card card);

    public event ToggleTurnDelegate ToggleTurnEvent;
    public event CardSelectedDelegate CardSelectedEvent;

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

    public void OnCardSelected(Card card) {
        var handler = this.PointerClickEvent;
        if (handler != null)
            handler(card);
    }
}
