using UnityEngine;

public class Card : ScriptableObject {

    public delegate void EnterDelegate();
    public event EnterDelegate EnterEvent;

    public delegate void ExitDelegate();
    public event ExitDelegate ExitEvent;

    public delegate void TurnStartDelegate();
    public event TurnStartDelegate TurnStartEvent;

    public delegate void TurnEndDelegate();
    public event TurnEndDelegate TurnEndEvent;

    public delegate void OutgoingDamageDelegate(CreatureCard src, Card target, ref int dmg);
    public event OutgoingDamageDelegate OutgoingDamageEvent;

    public delegate void DealDamageDelegate(CreatureCard src, Card target, int dmg);
    public event DealDamageDelegate DealDamageEvent;

    public delegate void DamageDealtDelegate(CreatureCard src, Card target, int dmg);
    public event DamageDealtDelegate DamageDealtEvent;

    public delegate void IncomingDamageDelegate(Card src, CreatureCard target, ref int dmg);
    public event IncomingDamageDelegate IncomingDamageEvent;

    public delegate void TakeDamageDelegate(Card src, CreatureCard target, int dmg);
    public event TakeDamageDelegate TakeDamageEvent;

    public delegate void DamageTakenDelegate(Card src, CreatureCard target, int dmg);
    public event DamageTakenDelegate DamageTakenEvent;

    public string Title;
    public string Desc;
    public string Flavor;
    public string[] Tags;

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
