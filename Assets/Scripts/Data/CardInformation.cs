using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "Cards", order = 1)]
public class CardInformation : ScriptableObject {

    public CardInformation card;
    public int id;
    public string imagePath;
    public string title;
    public string desc;
    public string flavor;
    public int number;

    public static bool operator == (CardInformation ci1, CardInformation ci2)
    {
        return ci1.title == ci2.title;
    }

    public static bool operator != (CardInformation ci1, CardInformation ci2)
    {
        return !(ci1 == ci2);
    }

    public static bool operator <= (CardInformation ci1, CardInformation ci2)
    {
        return string.Compare(ci1.title, ci2.title) <= 0;
    }

    public static bool operator >= (CardInformation ci1, CardInformation ci2)
    {
        return ci2 <= ci1;
    }

}
