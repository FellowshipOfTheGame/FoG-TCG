using UnityEngine;

public class State : MonoBehaviour {
    
    public StateMachine owner {
        get;
        private set;
    }

    public abstract void Enter();
    public abstract void Exit();

    void Awake() {
        owner = transform.parent.GetComponent<StateMachine>();
    }

    protected void SetState<T>() where T : State {
        owner.SetState<T>();
    }

}
