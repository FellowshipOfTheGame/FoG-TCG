using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine.UI;

public class Card : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

	public delegate void CardEventDelegate(Table args);

	public event CardEventDelegate EnterEvent;
	public event CardEventDelegate ExitEvent;
	public event CardEventDelegate TurnStartEvent;
	public event CardEventDelegate TurnEndEvent;

	public Table Data;

	public DynValue this[string attr] {
		get { return Data.Get(attr); }
		set { Data [attr] = DynValue.FromObject(board.luaEnv, value); }
	}

	public Board board;
    public Vector3 diff;

	// LoadScript MUST be called from the Board who creates the instance
	public void LoadScript(string name) {
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

    public void OnBeginDrag(PointerEventData eventData) {
        Slot.isChoosingPlace = true;
        diff = this.transform.position - Input.mousePosition;
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        this.transform.position = Input.mousePosition + diff;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (board.slot != null) {
            this.transform.SetParent(board.slot.transform);
            this.transform.position = board.slot.transform.position;
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
            Slot.isChoosingPlace = false;

            //this.OnEnter();
        }
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

	public void OnPointerClick(PointerEventData data) { // virtual?
       //board.manager.OnCardSelected(this);
	}

}