using UnityEngine;

public class CreatureCard : Card {

    [System.NonSerializedAttribute]
    public bool HasAttacked;

    [System.NonSerializedAttribute]
    public bool CanAttack;

    public int MaxHp;
    public int HP;
    public int Atk;

    public TerrainCard Terrain;

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

    public override bool Attack(Card target) {
        CreatureCard creature = target as CreatureCard;
        if (creature != null && this.CanAttack && !this.HasAttacked) {
            int dmg = this.Atk;

            OnOutgoingDamage(creature, ref dmg);
            Terrain.OnCreatureAttack(this, creature, ref dmg);
            OnDealDamage(creature, dmg);

            dmg = creature.TakeDamage(this, dmg);

            OnDamageDealt(creature, dmg);

            return true;
        }

        return false;
    }

    public override int TakeDamage(Card src, int dmg) {
        OnIncomingDamage(src, ref dmg);
        Terrain.OnCreatureAttacked(src, this, ref dmg);
        OnTakeDamage(src, dmg);
        
        HP -= dmg;

        OnDamageTaken(src, dmg);
        if (HP <= 0) {
            HP = 0;
            Die(src, dmg);
        }

        return dmg;
    }

}
