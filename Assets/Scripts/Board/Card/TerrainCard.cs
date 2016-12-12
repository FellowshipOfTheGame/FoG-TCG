using UnityEngine;

public class TerrainCard : Card {

    public delegate void CreatureAttackDelegate(CreatureCard src, CreatureCard target, ref int dmg);
    public delegate void CreatureAttackedDelegate(Card src, CreatureCard target, ref int dmg);

    public event CreatureAttackDelegate CreatureAttackEvent;
    public event CreatureAttackedDelegate CreatureAttackedEvent;
	
	public virtual void OnCreatureAttack(CreatureCard src, CreatureCard target, ref int dmg) {
		var handler = this.CreatureAttackEvent;
        if (handler != null)
            handler(src, target, ref dmg);
	}

    public virtual void OnCreatureAttacked(Card src, CreatureCard target, ref int dmg) {
		var handler = this.CreatureAttackedEvent;
        if (handler != null)
            handler(src, target, ref dmg);
	}

}
