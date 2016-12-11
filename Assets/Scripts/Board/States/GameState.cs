
public abstract class GameState : State {

    public BoardManager owner;
    public Board board { get { return owner.board; }}
    
    void Awake() {
        owner = transform.parent.GetComponent<GameManager>();
    }

}
