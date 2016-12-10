using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class DeckData{

    public static List<Deck> savedDecks = new List<Deck>();

    public static void Save()
    {
        savedDecks.Add(Deck.current);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedDecks.tcgd");
        bf.Serialize(file, DeckData.savedDecks);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "savedDecks.tcgd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedDecks.tcgd", FileMode.Open);
            DeckData.savedDecks = (List<Deck>)bf.Deserialize(file);
            file.Close();
        }
    }

}
