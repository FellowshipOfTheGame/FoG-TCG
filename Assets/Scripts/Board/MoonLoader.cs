using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MoonSharp.Interpreter;

public class MoonLoader : MonoBehaviour {

	[HideInInspector] public Script luaEnv;
	[HideInInspector] public ResourceData data;

	void Awake() {
        UserData.RegisterAssembly();
        luaEnv = new Script();
        luaEnv.Globals["GetCard"] = (Func<DynValue, Card>) (obj => { return GetCardFromObject(obj); }) ;
        luaEnv.Globals["print"] = (Action<DynValue>) (obj => { Print(obj); });
        //data = this.GetComponent<ResourceData>();
    }

	public static Card GetCardFromObject(DynValue obj) {
        return obj.ToObject<GameObject>().GetComponent<Card>();
    }
	
    public void Print(DynValue val) {
        print(val.ToString());
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
