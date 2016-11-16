using UnityEngine;

public class TerrainCard : Card {

    public delegate void CreatureAttackedDelegate(CreatureCard src, CreatureCard target, ref int dmg);
    public event CreatureAttackedDelegate CreatureAttackedEvent;

    public delegate void CreatureAttackDelegate(CreatureCard src, CreatureCard target, ref int dmg);
    public event CreatureAttackDelegate CreatureAttackEvent;

    public virtual void OnCreatureAttacked(CreatureCard src, CreatureCard target, ref int dmg) {
		var handler = this.CreatureAttackedEvent;
        if (handler != null)
            handler(src, target, dmg);
	}
	
	public virtual void OnCreatureAttack(CreatureCard src, CreatureCard target, ref int dmg) {
		var handler = this.CreatureAttackEvent;
        if (handler != null)
            handler(src, target, dmg);
	}

}
