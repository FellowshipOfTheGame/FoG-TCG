using UnityEngine;

public class CreatureCard : Card {

    public delegate void AttackDelegate();
    [HideInInspector]
    public event AttackDelegate AttackEvent;

    public delegate void DamageDelegate();
    [HideInInspector]
    public event DamageDelegate TakeDamageEvent;

    [HideInInspector]
    public bool HasAttacked;

    [HideInInspector]
    public bool CanAttack;

    public int MaxHp;
    public int HP;

    public override void OnEnter() {
        this.CanAttack = false;
        this.HasAttacked = false;
        base.Enter();
    }

    public override void OnTurnStart() {
        this.CanAttack = true;
        this.HasAttacked = false;
        base.TurnStart();
    }

    public void OnAttack() {
        if (this.CanAttack && !this.HasAttacked) {
            var handler = this.AttackEvent;
            if (handler != null)
                handler();

            // TODO attack

            this.HasAttacked = true;
        }
    }

    public void OnTakeDamage() {
        var handler = this.TakeDamageEvent;
        if (handler != null)
            handler();

        // TODO take damage
    }
}
