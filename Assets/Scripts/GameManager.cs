﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static char currScene= 'm';
    public static GameManager instance;

	//Variaveis de salvar deck
	public DeckListManager Deck;
	private DeckInformation savingDeck;
	public Dropdown Commander;
	public InputField DeckName;

	private string Deckpath;
	private string Cardpath;
	private string AnotherPath;

	//variaveis de carregar deck
	public Transform ListaDeDecks;
	public GameObject DeckPrefab;

	[HideInInspector] public MoonLoader loader;

    public static List<string> chosenDeck1;
    public static List<string> chosenDeck2;

	[HideInInspector] public string[] names = {"Player 1", "Player 2"};

    public void SaveDeck() {
		savingDeck = new DeckInformation ();

		//setando dados
		savingDeck.size = Deck.GetDeckSize();
		savingDeck.commander = Commander.options[Commander.value].text;
		savingDeck.name = DeckName.text;
		savingDeck.victories = 0;
		savingDeck.criationDate = System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year;

		for(int i = 0; i < Deck.transform.childCount; i++) {
			AddCardInformationMinimized aux = Deck.transform.GetChild (i).GetComponent<AddCardInformationMinimized> ();
			savingDeck.Cards.Add (new numCards(aux.title.text, aux.quantity));
		}

		//saving
		string JsonData = JsonUtility.ToJson (savingDeck);
		string filePath = Deckpath + savingDeck.name + ".json";
		File.WriteAllText (filePath, JsonData);

	}

	public void ValidateDeck(){
		if (DeckName.text.Length > 0 && Deck.GetDeckSize() > 0) {
			Deck.saveButton.SetActive(true);
		}else{
			Deck.saveButton.SetActive(false);
		}
	}


	public void SaveCard(CardInformation card) {
		
		string JsonData = JsonUtility.ToJson (card);
		string filePath2 = Cardpath + card.title + ".json";
		File.WriteAllText (filePath2, JsonData);

	}

	public void SaveInfos() {
		string JsonData = JsonUtility.ToJson (GameData.playerInfo);
		string filePath2 = AnotherPath + "config0" + ".json";
		File.WriteAllText (filePath2, JsonData);
	
	}

	public void DeleteDeck() {
		int index = 0;
		while (GameData.Decks [index++].name != LoadDeck.clickedDeck);

		DeckInformation deck = GameData.Decks [index-1];
		File.Delete (Deckpath + deck.name + ".json");

		LoadDecks ();
	}

	public void LoadDecks() {

		string[] ExistingDecksPaths = Directory.GetFiles(Deckpath , "*.json"); //pega todos os caminhos dos decks

		//esvazia a lista
		bool flagNovoDeck = true;
		foreach (Transform child in ListaDeDecks.transform) {
			if (!flagNovoDeck) {
				GameObject.Destroy (child.gameObject);
			}
			flagNovoDeck = false;
		}
		GameData.Decks.Clear ();

		foreach (string filePath in ExistingDecksPaths) {
			int index = filePath.LastIndexOf("/");
			string file = Deckpath + filePath.Substring(index+1);

			if (File.Exists (file)) {
				string JsonDeck = File.ReadAllText (file);
				DeckInformation aux = JsonUtility.FromJson<DeckInformation> (JsonDeck);

				GameObject DeckAux = Instantiate(DeckPrefab, ListaDeDecks);
				DeckAux.GetComponent<AddDeckInformation> ().deck = aux;

				GameData.Decks.Add (aux);
			}
		}
	}

	public void LoadCards() {
		/*
		string[] ExistingCardsPaths = Directory.GetFiles(Cardpath , "*.json");
		GameData.Cards.Clear ();

		foreach (string filePath in ExistingCardsPaths) {
			int index = filePath.LastIndexOf("/");
			string file = Cardpath + filePath.Substring(index+1);

			if (File.Exists (file)) {
				string JsonCard = File.ReadAllText (file);
				CardInformation aux = JsonUtility.FromJson<CardInformation> (JsonCard);

				//para mudar alguma coisa nas cartas, usar essa parte comentada
				//string JsonData = JsonUtility.ToJson (aux);
				//string filePath2 = Cardpath + aux.title + ".json";
				//File.WriteAllText (filePath2, JsonData);

				GameData.Cards.Add (aux);

				CardImage ci = new CardImage ();
				ci.card = aux.title;
				ci.imagem = Resources.Load<Sprite>(aux.title.ToLower());
				GameData.Images.Add (ci);
			}
		}
		*/

		TextAsset[] files = Resources.LoadAll<TextAsset>("MoonSharp/Scripts/");
		foreach( TextAsset card in files){
			CardInformation aux = new CardInformation();
			aux.LoadScript(loader, card.name);

			GameData.Cards.Add (aux);

			CardImage ci = new CardImage ();
			ci.card = aux.title;
			ci.imagem = Resources.Load<Sprite>(aux.title.ToLower());
			GameData.Images.Add (ci);
		}
		Debug.Log(GameData.Cards.Count);
		

		CardImage def = new CardImage ();
		def.card = "default";
		def.imagem = Resources.Load<Sprite>("default");
		if (def.imagem == null)
			Debug.Log ("No image found");
		GameData.Images.Add (def);
	}

	public void LoadInfos() {
		string[] ExistingConfigPaths = Directory.GetFiles(AnotherPath , "*.json");

		ConfigInformation config = new ConfigInformation ();

		if (ExistingConfigPaths.Length > 0) {
			foreach (string filePath in ExistingConfigPaths) {
				int index = filePath.LastIndexOf ("/");
				string file = AnotherPath + filePath.Substring (index + 1);

				if (File.Exists (file)) {
					string JsonCard = File.ReadAllText (file);
					config = JsonUtility.FromJson<ConfigInformation> (JsonCard);
				}

				GameData.playerInfo = config;
			}
		} else {
			string JsonData = JsonUtility.ToJson (config);
			string filePath2 = AnotherPath + "config0" + ".json";
			File.WriteAllText (filePath2, JsonData);

			GameData.playerInfo = config;
		}
	}

	public void SelectDeck(int i, string deck) {
        if (i == 1)
		    GameData.playerInfo.ActiveDeck1 = deck;
        else if (i == 2)
            GameData.playerInfo.ActiveDeck2 = deck;
        SaveInfos ();
	}

    void Awake() {
        if (instance != null) {
            GameManager gm = instance;
            gm.AnotherPath = Application.dataPath + "/Resources/";
            gm.Cardpath = Application.dataPath + "/";
           	gm.Commander = Commander;
            gm.Deck = Deck;
            gm.DeckName = DeckName;
           	gm.Deckpath = Application.dataPath + "/Decks/";
            gm.DeckPrefab = DeckPrefab;
           	gm.ListaDeDecks = ListaDeDecks;
            gm.savingDeck = savingDeck;
            Destroy(this.gameObject);
        } else {
            GameManager.instance = this.GetComponent<GameManager>();
			loader = this.gameObject.AddComponent<MoonLoader>();
            DontDestroyOnLoad(transform.gameObject);
            Deckpath = Application.dataPath + "/Decks/";
            Cardpath = Application.dataPath + "/";
            AnotherPath = Application.dataPath + "/Resources/";

            LoadCards();
            LoadInfos();
        }
    }

	public void ResetGameManager(){
		GameManager.instance = this.GetComponent<GameManager>();
            DontDestroyOnLoad(transform.gameObject);
            Deckpath = Application.dataPath + "/Decks/";
            Cardpath = Application.dataPath + "/";
            AnotherPath = Application.dataPath + "/Resources/";

            LoadCards();
            LoadInfos();
	}

    public void SetGameDeck() {
        if (GameData.playerInfo.ActiveDeck1 != string.Empty) {
            int index = 0;
            DeckInformation deck = new DeckInformation();
            if (GameData.Decks.Count > 0) {
                while (GameData.Decks[index++].name != GameData.playerInfo.ActiveDeck1) ;
                deck = GameData.Decks[index - 1];
            } else {
                string file = Deckpath + GameData.playerInfo.ActiveDeck1 + ".json";
                if (File.Exists(file)) {
                    string JsonDeck = File.ReadAllText(file);
                    deck = JsonUtility.FromJson<DeckInformation>(JsonDeck);
                }
            }
            chosenDeck1 = new List<string>();
            int i = 0, j = 0;
            index = 0;
            while (i < deck.size) {
                for (j = 0; j < deck.Cards[index].number; j++) {
                    chosenDeck1.Add(deck.Cards[index].name);
                }
                i += j;
                index++;
            }
        } else
            chosenDeck1 = null;

        if (GameData.playerInfo.ActiveDeck2 != string.Empty) {
            Debug.Log(GameData.playerInfo.ActiveDeck1);
            int index = 0;
            DeckInformation deck = new DeckInformation();
            if (GameData.Decks.Count > 0) {
                while (GameData.Decks[index++].name != GameData.playerInfo.ActiveDeck2) ;
                deck = GameData.Decks[index - 1];
            } else {
                string file = Deckpath + GameData.playerInfo.ActiveDeck2 + ".json";
                if (File.Exists(file)) {
                    string JsonDeck = File.ReadAllText(file);
                    deck = JsonUtility.FromJson<DeckInformation>(JsonDeck);
                }
            }
            chosenDeck2 = new List<string>();
            int i = 0, j = 0;
            index = 0;
            while (i < deck.size) {
                for (j = 0; j < deck.Cards[index].number; j++) {
                    chosenDeck2.Add(deck.Cards[index].name);
                }
                i += j;
                index++;
            }
        } else
            chosenDeck2 = null;
    }

}

public static class GameData {

	public static ConfigInformation playerInfo = new ConfigInformation();
	public static List<DeckInformation> Decks = new List<DeckInformation>();
	public static List<CardInformation> Cards = new List<CardInformation>();
	public static List<CardImage> Images = new List<CardImage>();

}
