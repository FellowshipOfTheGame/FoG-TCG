using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine.UI;

[MoonSharpUserData]
public class Card : MonoBehaviour {

	public delegate void CardEventDelegate(Table args);

	public event CardEventDelegate EnterEvent;
	public event CardEventDelegate ExitEvent;
	public event CardEventDelegate TurnStartEvent;
	public event CardEventDelegate TurnEndEvent;

    public event CardEventDelegate OutgoingDamageEvent;
    public event CardEventDelegate AttackEvent;
    public event CardEventDelegate DamageDealtEvent;

    public event CardEventDelegate IncomingDamageEvent;
    public event CardEventDelegate DefendEvent;
    public event CardEventDelegate DamageTakenEvent;

	public Table Data;

	public DynValue this[string attr] {
		get { return Data.Get(attr); }
		set { Data [attr] = DynValue.FromObject(board.luaEnv, value); }
	}

	public Board board;

    [Space(5)]
    public string infoName;
    public int cost;
    public int[] aspects;
    public int[] pos = new int[2];
    public char type;
    public int atk;
    public int hp;

	// LoadScript MUST be called from the Board who creates the instance
	public void LoadScript(string name) {
        infoName = name;
		Data = board.luaEnv.DoFile (name).Table;

		//SetDefaultValues ();
		RegisterDefaultEvents ();
	}
	/*
	void SetDefaultValues() {
		foreach (var pair in DEFAULT_ARGS)
			if (this[pair.Key].IsNil ())
				this [pair.Key] = pair.Value;
	}*/

	protected virtual void LoadAttributes() {
		// TODO load image	
	}

	protected virtual void RegisterDefaultEvents() {
		EnterEvent = delegate {};
		ExitEvent = delegate {};
		TurnStartEvent = delegate {};
		TurnEndEvent = delegate {};

		EnterEvent += LoadDefaultEventHandler ("OnEnter");
		ExitEvent += LoadDefaultEventHandler ("OnExit");
		TurnStartEvent += LoadDefaultEventHandler ("OnTurnStart");
		TurnEndEvent += LoadDefaultEventHandler ("OnTurnEnd");

        OutgoingDamageEvent = delegate { };
        AttackEvent = delegate { };
        DamageDealtEvent = delegate { };

        OutgoingDamageEvent += LoadDefaultEventHandler("OnOutgoingDamage");
        AttackEvent += LoadDefaultEventHandler("OnAttack");
        DamageDealtEvent += LoadDefaultEventHandler("OnDamageDealt");

        IncomingDamageEvent = delegate { };
        DefendEvent = delegate { };
        DamageTakenEvent = delegate { };

        IncomingDamageEvent += LoadDefaultEventHandler("OnIncomingDamage");
        DefendEvent += LoadDefaultEventHandler("OnDefendEvent");
        DamageTakenEvent += LoadDefaultEventHandler("OnDamageTaken");
    }

	protected CardEventDelegate LoadDefaultEventHandler(string eventName) {
		if (this [eventName].IsNil())
			return null;
		return args => this [eventName].Function.Call (args);
	}

	protected DynValue LoadAttribute(string attrName) {
		DynValue attr = this [attrName];
		if (attr.IsNil ())
			return null;
		return attr;
	}

    public virtual void OnEnter() {
        
		EnterEvent (null);
        
	}

	public virtual void OnExit() {
		ExitEvent (null);
	}

	public virtual void OnTurnStart() {
		TurnStartEvent (null);
	}

	public virtual void OnTurnEnd() {
		TurnEndEvent (null);
	}

	public virtual bool CanBePlayed() {
		return true;
	}

    private Table createTable(params object[] args)
    {
        return DynValue.FromObject(board.luaEnv, args as object[]).Table;
    }

    public void Attack()
    {
        int[] pos = transform.parent.GetComponent<Slot>().pos;
        GameObject targetGO = GetComponent<Card>().board.cardMatrix[3 - pos[0], pos[1]];

        if (targetGO == null)
        {
            // TODO tentar atacar comandante
        } else
        {
            Card target = targetGO.GetComponent<Card>();
            int dmg = this["atk"].ToObject<int>();
            Table args = createTable(board, this, target, dmg);

            this.OutgoingDamageEvent(args);
            print(args[4]);
            this.AttackEvent(args);
            print(args[4]);
            target.IncomingDamageEvent(args);
            print(args[4]);
            target.DefendEvent(args);
            print(args[4]);
            target.DamageTakenEvent(args);
            print(args[4]);
            this.DamageDealtEvent(args);
            print(args[4]);
        }
    }
}