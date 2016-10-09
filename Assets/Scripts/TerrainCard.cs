using UnityEngine;

public class TerrainCard : Card {

    public delegate void CreatureAttackedDelegate();
    public event CreatureAttackedDelegate CreatureAttackedEvent;

    public delegate void CreatureAttackDelegate();
    public event CreatureAttackDelegate CreatureAttackEvent;

    public void OnCreatureAttacked() {
		var handler = this.CreatureAttackedEvent;
        if (handler != null)
            handler();
	}
	
	public void OnCreatureAttack() {
		var handler = this.CreatureAttackEvent;
        if (handler != null)
            handler();
	}

}
