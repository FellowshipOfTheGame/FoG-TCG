
public class GameState : State {

    protected GameManager manager;
    
    void Awake() {
        owner = transform.parent.GetComponent<GameManager>();
    }
}
