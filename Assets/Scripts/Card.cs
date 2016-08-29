using UnityEngine;

public class Card : MonoBehaviour {

    public string title;
    public string desc;
    public string flavor;


    public delegate void EnterDelegate();
    [HideInInspector]
    public EnterDelegate OnEnter;

    public delegate void ExitDelegate();
    [HideInInspector]
    public ExitDelegate OnExit;

    public delegate void TurnStartDelegate();
    [HideInInspector]
    public TurnStartDelegate OnTurnStart;

    public delegate void TurnEndDelegate();
    [HideInInspector]
    public TurnEndDelegate OnTurnEnd;


    public virtual void Enter() {
        // Using this.onEnter is not thread safe
        // since there might be an interruption
        // between the check for null and the
        // call
        var handler = this.OnEnter;
        if (handler != null)
            handler();
    }

    public virtual void Exit() {
        var handler = this.OnExit;
        if (handler != null)
            handler();
    }

    public virtual void TurnStart() {
        var handler = this.OnTurnStart;
        if (handler != null)
            handler();
    }

    public virtual void TurnEnd() {
        var handler = this.OnTurnEnd;
        if (handler != null)
            handler();
    }

    public virtual bool CanBePlayed() {
        return true;
    }

}
