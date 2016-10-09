using UnityEngine;

public class CreatureCard : Card {

    public delegate void AttackDelegate();
    public event AttackDelegate AttackEvent;

    public delegate void DamageDelegate();
    public event DamageDelegate TakeDamageEvent;

    [System.NonSerializedAttribute]
    public bool HasAttacked;

    [System.NonSerializedAttribute]
    public bool CanAttack;

    public int MaxHp;
    public int HP;

    public override void OnEnter() {
        this.CanAttack = false;
        this.HasAttacked = false;
        base.OnEnter();
    }

    public override void OnTurnStart() {
        this.CanAttack = true;
        this.HasAttacked = false;
        base.OnTurnStart();
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
