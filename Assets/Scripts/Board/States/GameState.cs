
public abstract class GameState : State {

    public BoardManager boardManager;
    public Board board { get { return boardManager.board; }}
    
    void Awake() {
        boardManager = transform.parent.GetComponent<GameManager>();
    }

}
