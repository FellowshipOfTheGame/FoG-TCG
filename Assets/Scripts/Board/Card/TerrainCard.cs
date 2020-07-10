using UnityEngine;
using MoonSharp.Interpreter;

public class TerrainCard : Card {

	public event CardEventDelegate CreatureAttackEvent;
	public event CardEventDelegate CreatureTakeDamagedEvent;
	
	public virtual void OnCreatureAttack(CreatureCard src, CreatureCard target, ref int dmg) {
		DynValue luaArgs = DynValue.FromObject (board.loader.luaEnv, new object[] { src, target, dmg });
		CreatureAttackEvent(luaArgs.Table);
		dmg = (int) luaArgs.ToObject<object[]>()[2];
	}

    public virtual void OnCreatureTakeDamage(Card src, CreatureCard target, ref int dmg) {
		DynValue luaArgs = DynValue.FromObject (board.loader.luaEnv, new object[] { src, target, dmg });
		CreatureTakeDamagedEvent(luaArgs.Table);
		dmg = (int) luaArgs.ToObject<object[]>()[2];
	}

	protected override void RegisterDefaultEvents() {
		base.RegisterDefaultEvents ();
		CreatureAttackEvent = delegate {};
		CreatureTakeDamagedEvent = delegate {};

		CreatureAttackEvent += LoadDefaultEventHandler ("OnCreatureAttack");
		CreatureTakeDamagedEvent += LoadDefaultEventHandler ("OnCreatureAttacked");
	}

}
