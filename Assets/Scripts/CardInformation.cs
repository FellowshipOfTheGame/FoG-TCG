using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;

public class CardInformation {

	public string title;
	public string desc;
	public string flavor;
	public int number;
	public char type;
	public int cost;
	public int ATK;
	public int HP;
	public int manaToGive;
	public int qtdd;
	public int price;
	public int lvl;
	public int lvl_price;
	public int[] aspects;
	public int[] aspectsToGive;

	public Table Data;

	public void LoadScript(MoonLoader loader, string name) {
        title = name;
		Data = loader.luaEnv.DoFile (name).Table;

		desc = Data.Get("desc").ToObject<string>();
		flavor = Data.Get("flavor").ToObject<string>();
		type = Data.Get("type").ToObject<char>();

		desc = Data.Get("desc").ToObject<string>();
		flavor = Data.Get("flavor").ToObject<string>();
		type = Data.Get("type").ToObject<char>();

		aspects = Data.Get("aspects").ToObject<int[]>();
		aspectsToGive = new int[4];
		cost = Data.Get("cost").ToObject<int>();
        //this.GetComponent<AddCardInformationSemCanvas>().Initialize();

		aspectsToGive = Data.Get("elements").ToObject<int[]>();
		if (aspectsToGive == null) aspectsToGive = new int[4];

		if (type == 'c'){
			ATK = Data.Get("atk").ToObject<int>();
			HP = Data.Get("hp").ToObject<int>();
		}
		
		
	}

}
/*

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "Cards", order = 1)]
public class CardInformation : ScriptableObject {

    public string title;
    public string desc;
    public string flavor;
    public int number;
    public char type;
    public int cost;
    public int ATK;
    public int HP;
    public int manaToGive;
    public char[] aspects;
    public char[] aspectsToGive;
}
*/