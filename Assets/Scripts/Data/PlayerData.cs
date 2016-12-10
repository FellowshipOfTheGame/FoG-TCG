using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class PlayerData {

    public static string name;
    public static string level; //??
    public static SortedDictionary<CardInformation, int> collection = new SortedDictionary<CardInformation, int>();

}
