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

    public event CardEventDelegate NewCardInFieldEvent;

    public Table Data;
    public AnimationManager am;
	public DynValue this[string attr] {
		get { return Data.Get(attr); }
		set { Data[attr] = value; }
	}

	public Board board;
    public GameObject animPrefab;
    [Space(5)]
    public string infoName;
    public int cost;
    public int[] aspects;
    public int[] pos = new int[2];
    public char type;
    public int atk;
    public int hp;
    public bool canAttack;

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
		EnterEvent = delegate { canAttack = true; };
		ExitEvent = delegate {};
		TurnStartEvent = delegate { canAttack = true; };
		TurnEndEvent = delegate {};

		EnterEvent += LoadDefaultEventHandler ("OnEnter");
		ExitEvent += LoadDefaultEventHandler ("OnExit");
		TurnStartEvent += LoadDefaultEventHandler ("OnTurnStart");
		TurnEndEvent += LoadDefaultEventHandler ("OnTurnEnd");

        OutgoingDamageEvent = delegate { };
        AttackEvent = delegate { canAttack = false; };
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

        NewCardInFieldEvent = delegate { };
        NewCardInFieldEvent += LoadDefaultEventHandler("OnNewCardInField");
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

		EnterEvent (createTable(board, this));
	}

	public virtual void OnExit() {
        ExitEvent (createTable(board, this));
	}

	public virtual void OnTurnStart() {
        canAttack = true;
		TurnStartEvent (createTable(this));
	}

	public virtual void OnTurnEnd() {
        canAttack = false;
		TurnEndEvent (createTable(this));
	}

    public virtual void OnNewCardInField(Card newCard) {
        NewCardInFieldEvent(createTable(board, this, newCard));
    }

    public Table createTable(params object[] args)
    {
        return DynValue.FromObject(board.luaEnv, args as object[]).Table;
    }

    public void Attack()
    {
        if (!canAttack)
            return;
        Table args;
        GameObject targetGO = GetComponent<Card>().board.cardMatrix[3 - pos[0], pos[1]];
        
        int dmg = this["atk"].ToObject<int>();

        if (targetGO == null){
            Captain cap = board.players[2 - pos[0]].capt;
            if (Mathf.Abs(pos[1] - cap.pos) <= 1) {
                if (board.currPlayer == 1)
                    am.setAnim("atk");
                else
                    am.setAnim("atk2");

                Player target = board.players[2 - pos[0]];
                target.capt.anims[pos[1] - cap.pos + 1].setAnim("hit");
                args = createTable(board, this, target, dmg);
                this.OutgoingDamageEvent(args);
                this.AttackEvent(args);
                target.Damage(args);
                this.DamageDealtEvent(args);
            }

        } else {
            if (board.currPlayer == 1)
                am.setAnim("atk");
            else
                am.setAnim("atk2");

            Card target = targetGO.GetComponent<Card>();
            target.am.setAnim("hit");
            args = createTable(board, this, target, dmg);

            this.OutgoingDamageEvent(args);
            this.AttackEvent(args);
            target.Damage(args, false);
            this.DamageDealtEvent(args);
            
            if (target["hp"].ToObject<int>() <= 0)
            {
                //target.OnDeath();
                //target.Remove();
                target.am.setAnim("die");
            }
        }
        GameObject targetTerrain = GetComponent<Card>().board.cardMatrix[(pos[0] + 2) % 4, pos[1]];
        if (targetTerrain != null) {
            Card aux = targetTerrain.GetComponent<Card>();
            args = createTable(board, aux, this, dmg);
            aux.IncomingDamageEvent(args);
        }
    }

    public void Damage(Table args, bool checkDeath=true)
    {
        this.IncomingDamageEvent(args);
        //print(args[4]);
        this.DefendEvent(args);
        //print(args[4]);
        int currentHP = this["hp"].ToObject<int>();
        int newHP = currentHP - args.Get(4).ToObject<int>();
        this["hp"] = DynValue.FromObject(board.luaEnv, newHP);

        if (checkDeath && newHP <= 0) {
            //Remove();
            am.transform.SetParent(this.transform.parent);
            addAnimation("die");
        }
    }

    public void Remove(){
        if (type != 'a')
            board.cardMatrix[pos[0], pos[1]] = null;

        this.OnExit();
        Destroy(this.gameObject);
        print("DESTRUCTION");
    }

    public void changeAnimation(string anim) {
        am.setAnim(anim);
    }

    public void addAnimation(string anim) {
        GameObject temp = Instantiate(animPrefab, this.transform);
        AnimationManager am2 = temp.GetComponent<AnimationManager>();
        am2.LoadAll();
        am2.oneShot = true;
        am2.setAnim(anim);
    }
}