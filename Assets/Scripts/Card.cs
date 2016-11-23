using UnityEngine;

public class Card : ScriptableObject {

    public enum CardTag {
        // TODO colocar tipos
    };

    public delegate void EnterDelegate();
    public delegate void ExitDelegate();
    public delegate void TurnStartDelegate();
    public delegate void TurnEndDelegate();
    public delegate void OutgoingDamageDelegate(CreatureCard src, Card target, ref int dmg);
    public delegate void DealDamageDelegate(CreatureCard src, Card target, int dmg);
    public delegate void DamageDealtDelegate(CreatureCard src, Card target, int dmg);
    public delegate void IncomingDamageDelegate(Card src, CreatureCard target, ref int dmg);
    public delegate void TakeDamageDelegate(Card src, CreatureCard target, int dmg);
    public delegate void DamageTakenDelegate(Card src, CreatureCard target, int dmg);

    public event EnterDelegate EnterEvent;
    public event ExitDelegate ExitEvent;
    public event TurnStartDelegate TurnStartEvent;
    public event TurnEndDelegate TurnEndEvent;
    public event OutgoingDamageDelegate OutgoingDamageEvent;
    public event DealDamageDelegate DealDamageEvent;
    public event DamageDealtDelegate DamageDealtEvent;
    public event IncomingDamageDelegate IncomingDamageEvent;
    public event TakeDamageDelegate TakeDamageEvent;
    public event DamageTakenDelegate DamageTakenEvent;

    public string Title;
    public string Desc;
    public string Flavor;
    public CardTag[] Tags;

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

    public virtual bool Attack(Card target) {
        return false;
    }

    public virtual int TakeDamage(Card src, int dmg) {
        return dmg;
    }

    public virtual bool CanBePlayed() {
        return true;
    }

    protected void Die(Card src, int dmg) {
        // TODO notificar GameManager e tals
        // NÃO destruir GameObject
    }

    protected void OnIncomingDamage(Card src, ref int dmg) {
        var handler = this.IncomingDamageEvent;
        if (handler != null)
            handler(target, this, dmg);
    }

    protected void OnTakeDamage(Card src, int dmg) {
        var handler = this.TakeDamageEvent;
        if (handler != null)
            handler(target, this, dmg);
    }

    protected void OnDamageTaken(Card src, int dmg) {
        var handler = this.DamageTakenEvent;
        if (handler != null)
            handler(target, this, dmg);
    }

    protected void OnOutgoingDamage(Card target, ref int dmg) {
        var handler = this.OutgoingDamageEvent;
        if (handler != null)
            handler(this, target, dmg);
    }

    protected void OnDealDamage(Card target, int dmg) {
        var handler = this.DealDamageEvent;
        if (handler != null)
            handler(this, target, dmg);
    }

    protected void OnDamageDealt(Card target, int dmg) {
        var handler = this.DamageDealtEvent;
        if (handler != null)
            handler(this, target, dmg);
    }

}
