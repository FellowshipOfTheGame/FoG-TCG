
public abstract class GameState : State {

    public BoardManager owner {
        get;
        private set;
    }

    public Board board { get { return owner.board; }}

    void Awake() {
        owner = GetComponent<BoardManager>();
    }

    protected void SetState<T>() where T : State {
        owner.SetState<T>();
    }

    protected void SetState<T>(object arg) where T : State {
        owner.SetState<T>(arg);
    }

    protected void Return() {
        owner.Return();
    }

}
