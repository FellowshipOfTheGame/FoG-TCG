using UnityEngine;
using MoonSharp;
using MoonSharp.Interpreter;

public class CreatureCard : Card {
	
	public event CardEventDelegate OutgoingDamageEvent;
	public event CardEventDelegate DealDamageEvent;
	public event CardEventDelegate DamageDealtEvent;
	public event CardEventDelegate IncomingDamageEvent;
	public event CardEventDelegate TakeDamageEvent;
	public event CardEventDelegate DamageTakenEvent;

    public bool HasAttacked;
    public bool CanAttack;

    public int MaxHp;
    public int HP;
    public int Atk;

    public TerrainCard terrain;

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

	protected override void LoadAttributes() {
		base.LoadAttributes ();

		MaxHp = LoadAttribute ("HP").ToObject<int>();
		HP = MaxHp;
		Atk = LoadAttribute ("Atk").ToObject<int>();
	}

	protected override void RegisterDefaultEvents() {
		base.RegisterDefaultEvents ();

		OutgoingDamageEvent = delegate {
			HasAttacked = true;
		};
		DealDamageEvent = delegate {};
		DamageDealtEvent = delegate {};
		IncomingDamageEvent = delegate {};
		TakeDamageEvent = delegate {};
		DamageTakenEvent = delegate {};

		TurnStartEvent += delegate {
			HasAttacked = false;
			CanAttack = true;
		};

		OutgoingDamageEvent += LoadDefaultEventHandler ("OnOutgoingDamage");
		DealDamageEvent += LoadDefaultEventHandler ("OnDealDamage");
		DamageDealtEvent += LoadDefaultEventHandler ("OnDamageDealt");
		IncomingDamageEvent += LoadDefaultEventHandler ("OnIncomingDamage");
		TakeDamageEvent += LoadDefaultEventHandler ("OnTakeDamage");
		DamageTakenEvent += LoadDefaultEventHandler ("OnDamageTaken");
	}

    public void Attack(CreatureCard target) {
        if (target != null && this.CanAttack && !this.HasAttacked) {
            int dmg = this.Atk;

			OnOutgoingDamage(target, ref dmg);
            terrain.OnCreatureAttack(this, target, ref dmg);
			OnDealDamage (target, dmg);
			target.terrain.OnCreatureTakeDamage (this, target, ref dmg);
            dmg = target.TakeDamage(this, dmg);

            OnDamageDealt(target, dmg);
        }
    }

	public int TakeDamage(Card src, int dmg) {
		OnIncomingDamage(src, ref dmg);
		terrain.OnCreatureTakeDamage(src, this, ref dmg);
		OnTakeDamage(src, dmg);

		HP -= dmg;

		OnDamageTaken(src, dmg);
		if (HP <= 0) {
			HP = 0;
		}

		return dmg;
	}

	public void OnIncomingDamage(Card src, ref int dmg) {
		DynValue luaArgs = DynValue.FromObject (board.loader.luaEnv, new object[] { src, this, dmg });
		IncomingDamageEvent (luaArgs.Table);
		dmg = (int) luaArgs.ToObject<object[]>()[2];
	}

	public void OnTakeDamage(Card src, int dmg) {
		TakeDamageEvent (DynValue.FromObject(board.loader.luaEnv, new object[] { src, this, dmg }).Table);
	}

	public void OnDamageTaken(Card src, int dmg) {
		DamageTakenEvent (DynValue.FromObject(board.loader.luaEnv, new object[] { src, this, dmg }).Table);
	}

	public void OnOutgoingDamage(Card target, ref int dmg) {
		DynValue args = DynValue.FromObject(board.loader.luaEnv, new object[] { this, target, dmg });
		OutgoingDamageEvent (args.Table);
		dmg = (int)args.ToObject<object[]>() [2];
	}

	public void OnDealDamage(Card target, int dmg) {
		DealDamageEvent (DynValue.FromObject(board.loader.luaEnv, new object[] { this, target, dmg }).Table);
	}

	public void OnDamageDealt(Card target, int dmg) {
		DamageDealtEvent (DynValue.FromObject(board.loader.luaEnv, new object[] { this, target, dmg }).Table);
	}

}
