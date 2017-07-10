using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static char currScene= 'm';

	//Variaveis de salvar deck
	public Transform Deck;
	private DeckInformation savingDeck;
	public Dropdown Commander;
	public Text DeckName;

	private string Deckpath;
	private string Cardpath;
	private string AnotherPath;

	//variaveis de carregar deck
	public Transform ListaDeDecks;
	public GameObject DeckPrefab;

	public void SaveDeck() {
		savingDeck = new DeckInformation ();

		//setando dados
		savingDeck.size = Deck.GetComponent<DeckListManager>().GetDeckSize();
		savingDeck.commander = Commander.options[Commander.value].text;
		savingDeck.name = DeckName.text;
		savingDeck.victories = 0;
		savingDeck.criationDate = System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year;

		for(int i = 0; i < Deck.childCount; i++) {
			AddCardInformationMinimized aux = Deck.GetChild (i).GetComponent<AddCardInformationMinimized> ();
			savingDeck.Cards.Add (new numCards(aux.title.text, aux.quantity));
		}

		//saving
		string JsonData = JsonUtility.ToJson (savingDeck);
		string filePath = Deckpath + savingDeck.name + ".json";
		File.WriteAllText (filePath, JsonData);

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
		string[] ExistingCardsPaths = Directory.GetFiles(Cardpath , "*.json");
		GameData.Cards.Clear ();

		foreach (string filePath in ExistingCardsPaths) {
			int index = filePath.LastIndexOf("/");
			string file = Cardpath + filePath.Substring(index+1);

			if (File.Exists (file)) {
				string JsonCard = File.ReadAllText (file);
				CardInformation aux = JsonUtility.FromJson<CardInformation> (JsonCard);

				//para mudar alguma coisa nas cartas, usar essa parte comentada
				/*string JsonData = JsonUtility.ToJson (aux);
				string filePath2 = Cardpath + aux.title + ".json";
				File.WriteAllText (filePath2, JsonData);*/

				GameData.Cards.Add (aux);
			}


		}
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

	public void SelectDeck() {
		GameData.playerInfo.ActiveDeck = LoadDeck.clickedDeck;
		SaveInfos ();
	}

    void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		Deckpath = Application.dataPath + "/Dados/Decks/"; 
		Cardpath = Application.dataPath + "/Dados/Cards/";
		AnotherPath = Application.dataPath + "/Dados/"; 

		LoadCards ();
		LoadInfos ();
			
    }

}

public static class GameData {

	public static ConfigInformation playerInfo = new ConfigInformation();
	public static List<DeckInformation> Decks = new List<DeckInformation>();
	public static List<CardInformation> Cards = new List<CardInformation>();


}
