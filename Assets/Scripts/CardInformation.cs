using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public List<char> aspects = new List<char>();
	public List<char> aspectsToGive = new List<char>();

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