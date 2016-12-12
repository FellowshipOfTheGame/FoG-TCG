
public class GameEvent {

    private Action<Board, object> action;
    private object arg;

    public GameEvent(Action<Board, object> action, object arg) {
        this.action = action;
        this.arg = arg;
    }

    public void Execute(Board board) {
        this.action(board, this.arg);
    }
    
}
