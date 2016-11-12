using UnityEngine;

public class Card : ScriptableObject {

    public string Title;
    public string Desc;
    public string Flavor;


    public delegate void EnterDelegate();
    public event EnterDelegate EnterEvent;

    public delegate void ExitDelegate();
    public event ExitDelegate ExitEvent;

    public delegate void TurnStartDelegate();
    public event TurnStartDelegate TurnStartEvent;

    public delegate void TurnEndDelegate();
    public event TurnEndDelegate TurnEndEvent;


    public virtual void OnEnter() {
        // Using this.onEnter is not thread safe
        // since there might be an interruption
        // between the check for null and the
        // call
        var handler = this.EnterEvent;
        if (handler != null)
            handler();
    }

    public virtual void OnExit() {
        var handler = this.ExitEvent;
        if (handler != null)
            handler();
    }

    public virtual void OnTurnStart() {
        var handler = this.TurnStartEvent;
        if (handler != null)
            handler();
    }

    public virtual void OnTurnEnd() {
        var handler = this.TurnEndEvent;
        if (handler != null)
            handler();
    }

    public virtual bool OnCanBePlayed() {
        return true;
    }

}
