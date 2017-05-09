using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using MoonSharp.Interpreter;

public class Card : MonoBehaviour, IPointerClickHandler {

	protected static object[] NO_ARGS = new object[0];
	protected static CardTag[] NO_TAGS = new CardTag[0];

	private static IDictionary<string, object> DEFAULT_ARGS = new Dictionary<string, object> () {
		{"Aspects", new byte[] {0, 0, 0, 0}},
		{"Tags", NO_TAGS}
	};

	public delegate void CardEventDelegate(object[] args);
	/*
    public delegate void EnterDelegate();
    public delegate void ExitDelegate();
    public delegate void TurnStartDelegate();
    public delegate void TurnEndDelegate();
    public delegate void OutgoingDamageDelegate(Card src, Card target, ref int dmg);
    public delegate void DealDamageDelegate(Card src, Card target, int dmg);
    public delegate void DamageDealtDelegate(Card src, Card target, int dmg);
    public delegate void IncomingDamageDelegate(Card src, Card target, ref int dmg);
    public delegate void TakeDamageDelegate(Card src, Card target, int dmg);
    public delegate void DamageTakenDelegate(Card src, Card target, int dmg);*/

	public event CardEventDelegate EnterEvent;
	public event CardEventDelegate ExitEvent;
	public event CardEventDelegate TurnStartEvent;
	public event CardEventDelegate TurnEndEvent;
	public event CardEventDelegate OutgoingDamageEvent;
	public event CardEventDelegate DealDamageEvent;
	public event CardEventDelegate DamageDealtEvent;
	public event CardEventDelegate IncomingDamageEvent;
	public event CardEventDelegate TakeDamageEvent;
	public event CardEventDelegate DamageTakenEvent;

	public Table Data;

	public DynValue this[string attr] {
		get { return Data.Get(attr); }
		set { Data [attr] = DynValue.FromObject(board.LuaEnv, value); }
	}

    public Board board;

    void Awake() {
        board = gameObject.transform.parent.GetComponent<Board>();
    }

	// LoadScript MUST be called from the Board who creates the instance
	public void LoadScript(string name, Script s) {
		Data = s.DoFile (name).Table;

		SetDefaultValues ();
		RegisterDefaultEvents ();
	}

	void SetDefaultValues() {
		foreach (var pair in DEFAULT_ARGS)
			if (this[pair.Key].IsNil ())
				this [pair.Key] = pair.Value;
	}

	void RegisterDefaultEvents() {
		EnterEvent = delegate {};
		ExitEvent = delegate {};
		TurnStartEvent = delegate {};
		TurnEndEvent = delegate {};
		OutgoingDamageEvent = delegate {};
		DealDamageEvent = delegate {};
		DamageDealtEvent = delegate {};
		IncomingDamageEvent = delegate {};
		TakeDamageEvent = delegate {};
		DamageTakenEvent = delegate {};


		EnterEvent += LoadDefaultEventHandler ("OnEnter");
		ExitEvent += LoadDefaultEventHandler ("OnExit");
		TurnStartEvent += LoadDefaultEventHandler ("OnTurnStart");
		TurnEndEvent += LoadDefaultEventHandler ("OnTurnEnd");
		OutgoingDamageEvent += LoadDefaultEventHandler ("OnOutgoingDamage");
		DealDamageEvent += LoadDefaultEventHandler ("OnDealDamage");
		DamageDealtEvent += LoadDefaultEventHandler ("OnDamageDealt");
		IncomingDamageEvent += LoadDefaultEventHandler ("OnIncomingDamage");
		TakeDamageEvent += LoadDefaultEventHandler ("OnTakeDamage");
		DamageTakenEvent += LoadDefaultEventHandler ("OnDamageTaken");
	}

	CardEventDelegate LoadDefaultEventHandler(string eventName) {
		if (this [eventName].IsNil())
			return null;
		return args => this [eventName].Function.Call (args);
	}


	public virtual void OnEnter() {
		EnterEvent (NO_ARGS);
    }

    public virtual void OnExit() {
		ExitEvent (NO_ARGS);
    }

    public virtual void OnTurnStart() {
		TurnStartEvent (NO_ARGS);
    }

    public virtual void OnTurnEnd() {
		TurnEndEvent (NO_ARGS);
    }

    public virtual bool Attack(Card target) {
		// Revisar arquitetura, tentar delegar ao script
        return false;
    }

    public virtual int TakeDamage(Card src, int dmg) {
        return dmg;
    }

    public virtual bool CanBePlayed() {
        return true;
    }

    public void OnPointerClick(PointerEventData data) { // virtual?
        board.manager.OnCardSelected(this);
    }

    protected void Die(Card src, int dmg) {
        // TODO notify Board (maybe)
    }

    public void OnIncomingDamage(Card src, ref int dmg) {
		object[] args = new object[] { src, dmg };
		IncomingDamageEvent (args);
		dmg = (int)args [1];
    }

	public void OnTakeDamage(Card src, int dmg) {
		TakeDamageEvent (new object[] { src, dmg });
    }

	public void OnDamageTaken(Card src, int dmg) {
		DamageTakenEvent (new object[] { src, dmg });
    }

	public void OnOutgoingDamage(Card target, ref int dmg) {
		object[] args = new object[] { target, dmg };
		OutgoingDamageEvent (args);
		dmg = (int)args [1];
    }

	public void OnDealDamage(Card target, int dmg) {
		DealDamageEvent (new object[] { target, dmg });
    }

	public void OnDamageDealt(Card target, int dmg) {
		DamageDealtEvent (new object[] { target, dmg });
    }

}
