using System;

public class Card : MonoBehaviour {

    public string title;
    public string desc;
    public string flavor;

    public delegate void onEnter();
    public delegate void onExit();
    public delegate void onTurnStart();
    public delegate void onTurnEnd();

    public void enter() {
        // Using this.onEnter is not thread safe
        // since there might be an interruption
        // between the check for null and the
        // call
        var handler = this.onEnter;
        if (handler != null)
            handler();
    }

    public void exit() {
        var handler = this.onExit;
        if (handler != null)
            handler();
    }

    public void turnStart() {
        var handler = this.onTurnStart;
        if (handler != null)
            handler();
    }

    public void turnEnd() {
        var handler = this.onTurnEnd;
        if (handler != null)
            handler();
    }

    public boolean canBePlayed() {
        return true;
    }

}
