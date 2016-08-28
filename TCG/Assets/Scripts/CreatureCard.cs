using System;

public class CreatureCard : Card {


    [HideInSpector]
    public delegate void OnAttack();

    [HideInSpector]
    public delegate void OnTakeDamage();

    [HideInSpector]
    public boolean HasAttacked;

    [HideInSpector]
    public boolean CanAttack;

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
        if (this.canAttack && !this.hasAttacked) {
            var handler = this.OnAttack;
            if (handler != null)
                handler();

            // TODO attack

            this.hasAttacked = true;
        }
    }

    public void takeDamage() {
        var handler = this.OnTakeDamage;
        if (handler != null)
            handler();

        // TODO take damage
    }
}
