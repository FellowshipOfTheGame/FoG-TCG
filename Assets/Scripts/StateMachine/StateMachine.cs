using UnityEngine;

public class StateMachine : MonoBehaviour {

    private State _state;
    public State state {
        get {
            return state;
        }
        set {
            if (state != value) {
                if (state != null)
                    state.Exit();
                state = value;
                if (state != null)
                    state.Enter();
            }
        }
    }

    public T GetState<T>() where T : State {
        return GetComponent<T>() ?? gameObject.AddComponent<T>();
    }

    public void SetState<T>() where T : State {
        state = GetState<T>();
    }
}
