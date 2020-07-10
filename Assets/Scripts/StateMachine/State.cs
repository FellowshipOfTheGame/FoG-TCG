using UnityEngine;

public class State : MonoBehaviour {

    public StateMachine machine;
    public object[] args { get; private set; }

    public void Enter(params object[] args) {
        this.args = args;
        this.Enter();
    }

    public virtual void Enter() {}
    public virtual void Exit() {}

}
