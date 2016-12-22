using UnityEngine;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour {

    private Stack<Tuple<State, object[]>> prevStates =
                    new Stack<Tuple<State, object[]>>();

    private State _state;
    public State state {
        get {
            return state;
        }
        set {
            if (_state != value) {
                if (_state != null)
                    _state.Exit();
                prevStates.Push(_state);
                _state = value;
                if (_state != null)
                    _state.Enter();
            }
        }
    }

    public void ClearHistory() {
        prevStates.Clear();
    }

    public void Return() {
        if (prevStates.Count > 0) {
            if (_state != null)
                _state.Exit();
            var t = prevStates.Pop();
            _state = t.Item1;
            _state.arg = t.Item2;
            if (_state != null)
                _state.Enter();
        }
    }

    public T GetState<T>() where T : State {
        return GetComponent<T>() ?? gameObject.AddComponent<T>() as T;
    }

    public void SetState<T>() where T : State {
        SetState<T>(null);
    }

    public void SetState<T>(object[] arg) where T : State {
        state.arg = null;
        var s = GetState<T>();
        s.arg = arg;
        state = s;
    }
}
