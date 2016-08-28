using System;

public class TerrainCard : Card {

    [HideInSpector]
    public delegate void OnCreatureAttacked();

    [HideInSpector]
    public delegate void OnCreatureAttack();

}
