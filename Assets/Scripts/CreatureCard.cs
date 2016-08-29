using UnityEngine;

public class CreatureCard : Card {

    public delegate void AttackDelegate();
    [HideInInspector]
    public AttackDelegate OnAttack;

    public delegate void DamageDelegate();
    [HideInInspector]
    public DamageDelegate OnTakeDamage;

    [HideInInspector]
    public bool HasAttacked;

    [HideInInspector]
    public bool CanAttack;

    public int MaxHp;
    public int HP;

    public override void Enter() {
        this.CanAttack = false;
        this.HasAttacked = false;
        base.Enter();
    }

    public override void TurnStart() {
        this.CanAttack = true;
        this.HasAttacked = false;
        base.TurnStart();
    }

    public void Attack() {
        if (this.CanAttack && !this.HasAttacked) {
            var handler = this.OnAttack;
            if (handler != null)
                handler();

            // TODO attack

            this.HasAttacked = true;
        }
    }

    public void TakeDamage() {
        var handler = this.OnTakeDamage;
        if (handler != null)
            handler();

        // TODO take damage
    }
}
