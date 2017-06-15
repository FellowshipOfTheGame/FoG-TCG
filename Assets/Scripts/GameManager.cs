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

				GameData.Cards.Add (aux);
			}

		}
	}

    void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		Deckpath = Application.dataPath + "/Dados/Decks/"; 
		Cardpath = Application.dataPath + "/Dados/Cards/"; 

		LoadCards ();
			
    }

}

public static class GameData {

	public static string ActiveDeck;
	public static List<DeckInformation> Decks = new List<DeckInformation>();
	public static List<CardInformation> Cards = new List<CardInformation>();


}
