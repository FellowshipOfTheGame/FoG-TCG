using UnityEngine;

public class State : MonoBehaviour {
    
    public StateMachine owner {
        get;
        private set;
    }

    public virtual void Enter() {}
    public virtual void Exit() {}

    void Awake() {
        owner = GetComponent<StateMachine>();
    }

    protected void SetState<T>() where T : State {
        owner.SetState<T>();
    }

}
