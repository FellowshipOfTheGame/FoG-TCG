using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "Cards", order = 1)]
public class CardScriptableObject : ScriptableObject {

    public string title;
    public string desc;
    public string flavor;
}
