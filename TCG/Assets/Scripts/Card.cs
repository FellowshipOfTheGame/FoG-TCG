using System;

public class Card : MonoBehaviour {

    public string title;
    public string desc;
    public string flavor;


    public delegate void OnEnter();

    public delegate void OnExit();

    public delegate void OnTurnStart();
    public delegate void OnTurnEnd();


    public void Enter() {
        // Using this.onEnter is not thread safe
        // since there might be an interruption
        // between the check for null and the
        // call
        var handler = this.OnEnter;
        if (handler != null)
            handler();
    }

    public void Exit() {
        var handler = this.OnExit;
        if (handler != null)
            handler();
    }

    public void TurnStart() {
        var handler = this.OnTurnStart;
        if (handler != null)
            handler();
    }

    public void TurnEnd() {
        var handler = this.OnTurnEnd;
        if (handler != null)
            handler();
    }

    public boolean CanBePlayed() {
        return true;
    }

}
