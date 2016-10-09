using UnityEngine;

public class TerrainCard : Card {

    public delegate void CreatureAttackedDelegate();
    [HideInInspector]
    public event CreatureAttackedDelegate OnCreatureAttacked;

    public delegate void CreatureAttackDelegate();
    [HideInInspector]
    public event CreatureAttackDelegate OnCreatureAttack;

    public void CreatureAttacked() {
		var handler = this.OnCreatureAttacked;
        if (handler != null)
            handler();
	}
	
	public void CreatureAttack() {
		var handler = this.OnCreatureAttack;
        if (handler != null)
            handler();
	}

}
