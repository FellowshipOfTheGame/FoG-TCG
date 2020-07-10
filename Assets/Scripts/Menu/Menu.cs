using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	public Button Config;

	public GameObject GOMenu;
	public GameObject GOJogar;

    public GameObject GOplayer1;
	public GameObject GOCartas;
    public GameObject GOEscolher;
	public GameObject GOComprarC;
	public GameObject GOGaleria;
	public GameObject GODecks;
	public GameObject GONovoDeck;
    public GameObject GOLatDeck;
    public GameObject GOLatCriaDeck;
	public GameObject GOStats;
	public GameObject GOConta;
	public GameObject GOConfig;
    public GameObject GOConfigMenu;
	public GameObject GOOpcoes;
	public GameObject GOCredit;
	public GameObject GOLogin;
	public GameObject GOCadast;
    public GameObject GOConfirmSair;
    public Book1C[] books;

    public Button[] bookButtons;
	private EventSystem ES;
	public PlaySceneMusic Music;
    public Dropdown[] selDecks;
	public GameManager GM;

    public GameObject blockConfig;

    public GameObject cardMinPrefab;
    
    public GameObject[] playDeckPlace;

    public Animator outdoor;

    int cBook = 0;

    bool configOpen = false, configTabOpen = false, gameStarted = false;

    void Start(){
        gameStarted = false;
    }
	void Update() {
		ES = EventSystem.current;
        /*
		if (ES.currentSelectedGameObject == null){
			if (GOMenu.activeInHierarchy)
				ES.SetSelectedGameObject (Play.gameObject);

			if (GOConfig.activeInHierarchy)
				ES.SetSelectedGameObject (VoltarConfig.gameObject);

			if (GOCredit.activeInHierarchy)
				ES.SetSelectedGameObject (VoltarCred.gameObject);
		}*/

        if (!gameStarted && Input.GetKeyDown(KeyCode.Return)){
            outdoor.SetTrigger("start");
            gameStarted = true;
        }
	}

	public void PlaySelected() {
        GameManager.instance.SetGameDeck();
        //SceneManager.LoadScene("Board"); // carregar jogo(trocar Game pelo nome da scene)
        outdoor.SetTrigger("play");
        Invoke("StartGame", 0.5f);
	}

    void StartGame(){
        SceneManager.LoadScene("Board"); // carregar jogo(trocar Game pelo nome da scene)
    }

	public void Desativar() {
		GOJogar.SetActive (false);
		GOCartas.SetActive (false);
		GOComprarC.SetActive (false);
		GOGaleria.SetActive (false);
		GODecks.SetActive (false);
		GONovoDeck.SetActive (false);
		GOStats.SetActive (false);
		GOConta.SetActive (false);
		GOConfig.SetActive (false);
		GOOpcoes.SetActive (false);
		GOCredit.SetActive (false);
		GOLogin.SetActive (false);
		GOCadast.SetActive (false);
		GOMenu.SetActive (false);
	}

    public void MudarLivro(int book) {
        books[cBook].transform.parent.gameObject.SetActive(false);
        bookButtons[cBook].interactable = true;
        cBook = book;
        bookButtons[cBook].interactable = false;
        books[cBook].transform.parent.gameObject.SetActive(true);
    }

	public void JogarSelected() {
        if (configOpen) ResetConfig();
        Config.interactable = false;
        GameManager.instance.LoadDecks();
        int a = -1, b = -1;
        for (int i = 0; i < GameData.Decks.Count; i++) {
            selDecks[0].options.Add(new Dropdown.OptionData(GameData.Decks[i].name));
            if (GameData.Decks[i].name == GameData.playerInfo.ActiveDeck1)
                a = i;
            selDecks[1].options.Add(new Dropdown.OptionData(GameData.Decks[i].name));
            if (GameData.Decks[i].name == GameData.playerInfo.ActiveDeck2)
                b = i;
        }
        if (a != -1) selDecks[0].value = a;
        else selDecks[0].value = 0;
        selDecks[0].RefreshShownValue();
        LoadPlayDeck(0, selDecks[0].captionText.text);

        if (b != -1) selDecks[1].value = b;
        else selDecks[1].value = 0;
        selDecks[1].RefreshShownValue();
        LoadPlayDeck(1, selDecks[1].captionText.text);

        cBook = 0;
		GOJogar.SetActive (true);

        books[0].nextPage = GOplayer1;
        books[0].changePage();

        books[0].anim.SetTrigger("Open");
	}

	public void CartasSelected() {
        if (configOpen) ResetConfig();
        Config.interactable = false;
        cBook = 1;
		GOCartas.SetActive (true);
        GOComprarC.SetActive(false);
        GOGaleria.SetActive(false);
        books[1].nextPage = GOEscolher;
        books[1].changePage();
        books[1].nextLateral = null;
        books[1].anim.SetTrigger("Open");
	}

    public void VoltarCartas() {
        books[1].nextPage = GOEscolher;
        books[1].nextLateral = null;
        books[1].anim.SetTrigger("Back");
    }

	public void RegrasSelected(GameObject pagina) {
        books[1].nextPage = pagina;
        books[1].anim.SetTrigger("Go");
	}

    public void MudarPagRegras(GameObject novaPagina){
        books[1].nextPage = novaPagina;
        books[1].anim.SetTrigger("Next");
    }

    public void VoltarPagRegras(GameObject novaPagina){
        books[1].nextPage = novaPagina;
        books[1].anim.SetTrigger("Prev");
    }

    public void MudarLatRegras(GameObject novaLat){
        books[1].nextLateral = novaLat;
    }

    public void MudarLatDeck(GameObject novaLat){
        books[2].nextLateral = novaLat;
    }

    public void MudarPagJogar(GameObject novaPagina){
        books[0].nextPage = novaPagina;
        books[0].anim.SetTrigger("Next");
    }

    public void VoltarPagJogar(GameObject novaPagina){
        books[0].nextPage = novaPagina;
        books[0].anim.SetTrigger("Prev");
    }

	public void GaleriaSelected() {
        books[1].nextPage = GOGaleria;
        books[1].anim.SetTrigger("Go");
        GOGaleria.SetActive (true);
	}

	public void DecksSelected() {
        if (configOpen) ResetConfig();
        Config.interactable = false;
        cBook = 2;
        CollectionDraggable.canDrag = false;
        GODecks.SetActive(false);
        GONovoDeck.SetActive(false);
        books[2].nextPage = GODecks;
        books[2].nextSide = GOLatDeck;
        books[2].anim.SetTrigger("Open");
        GameManager.instance.LoadDecks ();
	}

    public void ChooseDeck (int i) {
        LoadPlayDeck(i, selDecks[i].captionText.text);
        GameManager.instance.SelectDeck(i + 1, selDecks[i].captionText.text);
    }
     public void LoadPlayDeck(int player, string deckName){
		int index = 0;
		while (GameData.Decks [index++].name != deckName);

		DeckInformation deck = GameData.Decks [index-1];

		foreach (Transform child in playDeckPlace[player].transform) {
			GameObject.Destroy(child.gameObject);
		}

		for (int i = 0; i < deck.Cards.Count; i++) {
			GameObject aux = Instantiate (cardMinPrefab, playDeckPlace[player].transform);

			int aux2=0;
			while (GameData.Cards [aux2++].title != deck.Cards[i].name && aux2 <= GameData.Cards.Count);

			aux.GetComponent<AddCardInformationMinimized> ().card = GameData.Cards [aux2-1];
			aux.GetComponent<AddCardInformationMinimized> ().quantity = deck.Cards[i].number;
        }
    }

    public void VoltarDeck() {
        CollectionDraggable.canDrag = false;
        books[2].nextPage = GODecks;
        books[2].nextSide = GOLatDeck;
        books[2].anim.SetTrigger("Prev");
        GameManager.instance.LoadDecks();
    }

	public void NovoDeckSelected() {
        CollectionDraggable.canDrag = true;
        CollectionDraggable.collectionZone = GOLatCriaDeck.transform.Find("Cards").Find("ScrollContent").GetChild(0).gameObject;
        CollectionDraggable.deckListManager = CollectionDraggable.collectionZone.GetComponent<DeckListManager>();
        CollectionDraggable.canvas = this.transform.GetComponent<Canvas>();
        books[2].nextPage = GONovoDeck;
        books[2].nextSide = GOLatCriaDeck;
        books[2].anim.SetTrigger("Next");
	}

    public void SalvarDeck() {
        GameManager.instance.SaveDeck();
        VoltarDeck();
    }

    public void DeletarDeck() {
        GameManager.instance.DeleteDeck();
    }

    public void ConfigSelected() {
        configOpen = true;
        GOConfig.GetComponent<Animator>().SetTrigger("Move");
    }

    public void VoltarConfiguracao() {
        configOpen = false;
        GOConfig.GetComponent<Animator>().SetTrigger("Move");
    }

    public void VoltarAba() {
        configTabOpen = false;
        GOConfig.GetComponent<Animator>().SetTrigger("Prev");
    }

    public void ResetConfig(){
        blockConfig.SetActive(true);
    }

    public void StatsSelected() {
        Pergaminho.activeTab = GOStats.GetComponent<Animator>();
        GOConfig.GetComponent<Animator>().SetTrigger("Next");
	}

	public void ContaSelected() {
		Desativar();
		GOConta.SetActive (true);
	}

	public void OpcoesSelected() {
        configTabOpen = true;
        Pergaminho.activeTab = GOOpcoes.GetComponent<Animator>();
        GOConfig.GetComponent<Animator>().SetTrigger("Next");
    }

	public void CreditsSelected() {
        configTabOpen = true;
        Pergaminho.activeTab = GOCredit.GetComponent<Animator>();
        GOConfig.GetComponent<Animator>().SetTrigger("Next");
    }

	public void LoginSelected() {
		Desativar();
		GOLogin.SetActive (true);		
	}

	public void CadastSelected() {
		Desativar();
		GOCadast.SetActive (true);	
	}

	public void VoltarMenu() {
        books[cBook].anim.SetTrigger("Close");
        books[2].nextLateral = null;
        Config.interactable = true;
        blockConfig.SetActive(false);
		GOMenu.SetActive (true);
	}

    public void SureExit() {
        GOConfirmSair.GetComponent<Animator>().SetTrigger("Move");
    }

    public void ConfirmExit() {
        GOConfirmSair.GetComponent<Animator>().SetTrigger("Exit");
    }

	public void ExitSelected() {
		Application.Quit ();
	}

}
