using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "Cards", order = 1)]
public class CardInformation : ScriptableObject {

    public string title;
    public string desc;
    public string flavor;
    public int number;
    public char type;
}
