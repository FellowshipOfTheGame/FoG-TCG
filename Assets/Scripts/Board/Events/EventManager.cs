using System.Collections.Generic;

public class EventManager : MonoBehaviour {
    
    private Queue<GameEvent> pendingEvents;
    private Board board;

    void Awake() {
        pendingEvents = new Queue<GameEvent>();
        board = transform.parent.GetComponent<Board>();
    }

    public void Enqueue(GameEvent e) {
        pendingEvents.Enqueue(e);
    }

    public bool ProcessNextEvent() {
        if (pendingEvents.Count > 0)
            pendingEvents.Dequeue().Execute(board);

        return pendingEvents.Count > 0;
    }

    public void ProcessPendingEvents() {
        while (ProcessNextEvent())
            ; // TODO WaitForAnimation
    }

}
