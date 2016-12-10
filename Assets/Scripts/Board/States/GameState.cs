
public abstract class GameState : State {

    public BoardManager boardManager;
    public Board board { get { return boardManager.board; }}
    
    void Awake() {
        boardManager = transform.parent.GetComponent<GameManager>();
    }

    public virtual void Enter() {
        base.Enter();
        AddListeners();
    }

    public virtual void Exit() {
        RemoveListeners();
        base.Exit();
    }

    protected abstract void AddListeners();
    protected abstract void RemoveListeners();
}
