using UnityEngine;
using System.Collections;

public class TerrainCard : Card {

    public ArrayList atributesToGive;

    public delegate void CreatureAttackedDelegate();
    [HideInInspector]
    public event CreatureAttackedDelegate CreatureAttackedEvent;

    public delegate void CreatureAttackDelegate();
    [HideInInspector]
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
