using UnityEngine;
using UnityEngine.EventSystems;

public sealed class Board : MonoBehaviour {

    public BoardManager manager;

    private Player[] players;
    public Player P1 {
        get { return players[0]; }
    }
    public Player P2 {
        get { return players[1]; }
    }
    
    public Player CurrentPlayer {
        get { return players[manager.turn]; }
    }
    public Player OppositePlayer {
        get { return players[~manager.turn & 1]; }
    }

    void Awake() {
        manager = gameObject.AddComponent<BoardManager>() as BoardManager;
        GameObject obj = new GameObject("Event Manager", typeof(EventManager));
        obj.transform.parent = gameObject;
    }

}
