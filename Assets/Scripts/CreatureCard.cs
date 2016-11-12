using UnityEngine;

public class CreatureCard : Card, IDamageable, IDamageSource {

    public delegate void OutgoingDamageDelegate(CreatureCard src, IDamageable target, ref int dmg);
    public event OutgoingDamageDelegate OutgoingDamageEvent;

    public delegate void DealDamageDelegate(CreatureCard src, IDamageable target, int dmg);
    public event DealDamageDelegate DealDamageEvent;

    public delegate void DamageDealtDelegate(CreatureCard src, IDamageable target, int dmg);
    public event DamageDealtDelegate DamageDealtEvent;

    public delegate void IncomingDamageDelegate(IDamageSource src, CreatureCard target, ref int dmg);
    public event IncomingDamageDelegate IncomingDamageEvent;

    public delegate void TakeDamageDelegate(IDamageSource src, CreatureCard target, int dmg);
    public event TakeDamageDelegate TakeDamageEvent;

    public delegate void DamageTakenDelegate(IDamageSource src, CreatureCard target, int dmg);
    public event DamageTakenDelegate DamageTakenEvent;


    [System.NonSerializedAttribute]
    public bool HasAttacked;

    [System.NonSerializedAttribute]
    public bool CanAttack;

    public int MaxHp;
    public int HP;
    public int Atk;

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

    public bool IDamageSource.Attack(IDamageable target) {
        if (this.CanAttack && !this.HasAttacked) {
            int dmg = this.Atk;

            OnOutgoingDamage(target, ref dmg);
            OnDealDamage(target, dmg);

            dmg = target.TakeDamage(this, dmg);

            OnDamageDealt(target, dmg);

            return true;
        }

        return false;
    }

    public int IDamageable.TakeDamage(IDamageSource src, int dmg) {
        OnIncomingDamage(src, ref dmg);
        OnTakeDamage(src, dmg);
        
        HP -= dmg;

        OnDamageTaken(src, dmg);
        if (HP <= 0) {
            HP = 0;
            Die(src, dmg);
        }

        return dmg;
    }

    protected void Die(IDamageSource src, int dmg) {
        // TODO notificar GameManager e tals
        // NÃO destruir GameObject
    }



    protected void OnIncomingDamage(IDamageSource src, ref int dmg) {
        var handler = this.IncomingDamageEvent;
        if (handler != null)
            handler(target, this, dmg);
    }

    protected void OnTakeDamage(IDamageSource src, int dmg) {
        var handler = this.TakeDamageEvent;
        if (handler != null)
            handler(target, this, dmg);
    }

    protected void OnDamageTaken(IDamageSource src, int dmg) {
        var handler = this.DamageTakenEvent;
        if (handler != null)
            handler(target, this, dmg);
    }

    protected void OnOutgoingDamage(IDamageable target, ref int dmg) {
        var handler = this.OutgoingDamageEvent;
        if (handler != null)
            handler(this, target, dmg);
    }

    protected void OnDealDamage(IDamageable target, int dmg) {
        var handler = this.DealDamageEvent;
        if (handler != null)
            handler(this, target, dmg);
    }

    protected void OnDamageDealt(IDamageable target, int dmg) {
        var handler = this.DamageDealtEvent;
        if (handler != null)
            handler(this, target, dmg);
    }

}
