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

        //TEMPORARY
        //WHEN WE HAVE PLAYER PROFILE/COLLECTION THIS WILL GO THERE
        for (int i = 0; i < GameData.dataBase.Count; i++) {

            PlayerData.collection[GameData.dataBase[i].card] = 3;
        }
    }
}
