using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

public class DataBaseManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

        StreamReader sr = new StreamReader("Assets/Files/Database.txt");

        while (!sr.EndOfStream)
        {
            string lineReader = sr.ReadLine();
            CardInformation card = (CardInformation)AssetDatabase.LoadAssetAtPath("Assets/Scriptable Objects/" + lineReader + ".asset", typeof(CardInformation));
            GameData.dataBase.Add(card);
        }

        sr.Close();

        for(int i = 0; i < GameData.dataBase.Count; i++)
        {
            print(GameData.dataBase[i].title);
        }
    }
}
