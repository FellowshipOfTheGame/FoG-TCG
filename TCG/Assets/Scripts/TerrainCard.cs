using System;

public class TerrainCard : Card {

    [HideInSpector]
    public delegate void OnCreatureAttacked();

    [HideInSpector]
    public delegate void OnCreatureAttack();
	
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
