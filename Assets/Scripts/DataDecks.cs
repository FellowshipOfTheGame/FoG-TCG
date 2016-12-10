using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class DataDecks{

    public static List<Deck> SavedDecks = new List<Deck>();

    public static void Save()
    {
        SavedDecks.Add(Deck.current);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedDecks.tcgd");
        bf.Serialize(file, DataDecks.SavedDecks);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "savedDecks.tcgd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedDecks.tcgd", FileMode.Open);
            DataDecks.SavedDecks = (List<Deck>)bf.Deserialize(file);
            file.Close();
        }
    }

}
