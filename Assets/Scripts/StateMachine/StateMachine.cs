using UnityEngine;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour {

    public static readonly object[] emptyArray = new object[0];

    private Stack<Tuple<State, object[]>> prevStates =
                    new Stack<Tuple<State, object[]>>();

    public State state {
        get;
        private set;
    }

    public void ClearHistory() {
        prevStates.Clear();
    }

    public void Return() {
        if (prevStates.Count > 0) {
            if (state != null)
                state.Exit();
            var t = prevStates.Pop();
            state = t.Item1;
            if (state != null)
                state.Enter(t.Item2);
        }
    }

    public T GetState<T>() where T : State {
        var s = GetComponent<T>();
        if (s == null) {
            s = gameObject.AddComponent<T>() as T;
            s.machine = this;
        }
        return s;
    }

    public void SetState<T>(params object[] arg) where T : State {
        var s = GetState<T>();
        if (state != null) {
            prevStates.Push(new Tuple<State, object[]>(state, state.args));
            state.Exit();
        } else {
            prevStates.Push(new Tuple<State, object[]>(null, null));
        }

        state = s;
        if (state != null)
            state.Enter();
    }
}
