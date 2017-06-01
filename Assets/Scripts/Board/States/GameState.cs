
public abstract class GameState : State {

    public BoardManager bm {
        get;
        private set;
    }

    public Board board { get { return bm.board; }}

    void Awake() {
        bm = GetComponent<BoardManager>();
    }

    protected void SetState<T>() where T : State {
        bm.SetState<T>();
    }

    protected void SetState<T>(params object[] arg) where T : State {
        bm.SetState<T>(arg);
    }

    protected void Return() {
        bm.Return();
    }

}
