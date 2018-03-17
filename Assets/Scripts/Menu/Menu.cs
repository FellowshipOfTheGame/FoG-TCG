using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	public Button Play;
	public Button Camp;
	public Button VoltarStats;
	public Button VoltarConta;
	public Button VoltarConfig;
	public Button VoltarDecks;
	public Button VoltarOpt;
	public Button VoltarCred;
	public Button Login;
	public Button Cadast;

	public GameObject GOMenu;
	public GameObject GOJogar;
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
	private EventSystem ES;
	public PlaySceneMusic Music;
	public GameManager GM;

    int cBook = 0;
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
	}

	public void PlaySelected() {
        GM.SetGameDeck();
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

    public void MudarLivro() {
        books[cBook].transform.parent.gameObject.SetActive(false);
        cBook++;
        if (cBook > 2)
            cBook = 0;

        books[cBook].transform.parent.gameObject.SetActive(true);
    }

	public void JogarSelected() {
        cBook = 0;
		GOJogar.SetActive (true);
        books[0].anim.SetTrigger("Open");
	}

	public void CartasSelected() {
        cBook = 1;
		GOCartas.SetActive (true);
        GOComprarC.SetActive(false);
        GOGaleria.SetActive(false);
        books[1].nextPage = GOEscolher;
        books[1].changePage();
        books[1].anim.SetTrigger("Open");
	}

    public void VoltarCartas() {
        books[1].nextPage = GOEscolher;
        books[1].anim.SetTrigger("Back");
    }

	public void ComprarCSelected() {
        books[1].nextPage = GOComprarC;
        books[1].anim.SetTrigger("Go");
	}

	public void GaleriaSelected() {
        books[1].nextPage = GOGaleria;
        books[1].anim.SetTrigger("Go");
        GOGaleria.SetActive (true);
	}

	public void DecksSelected() {
        cBook = 2;
        CollectionDraggable.canDrag = false;
        GODecks.SetActive(false);
        GONovoDeck.SetActive(false);
        books[2].nextPage = GODecks;
        books[2].nextSide = GOLatDeck;
        books[2].anim.SetTrigger("Open");
		GM.LoadDecks ();
	}

    public void VoltarDeck() {
        CollectionDraggable.canDrag = false;
        books[2].nextPage = GODecks;
        books[2].nextSide = GOLatDeck;
        books[2].anim.SetTrigger("Prev");
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

    public void ConfigSelected() {
        GOConfig.GetComponent<Animator>().SetTrigger("Move");
    }

    public void VoltarConfiguracao() {
        GOConfig.GetComponent<Animator>().SetTrigger("Move");
    }

    public void VoltarAba() {
        GOConfig.GetComponent<Animator>().SetTrigger("Prev");
    }

    public void StatsSelected() {
        Pergaminho.activeTab = GOStats.GetComponent<Animator>();
        GOConfig.GetComponent<Animator>().SetTrigger("Next");
	}

	public void ContaSelected() {
		Desativar();
		GOConta.SetActive (true);
		ES.SetSelectedGameObject (VoltarConfig.gameObject);
	}

	public void OpcoesSelected() {
        Pergaminho.activeTab = GOOpcoes.GetComponent<Animator>();
        GOConfig.GetComponent<Animator>().SetTrigger("Next");
    }

	public void CreditsSelected() {
        Pergaminho.activeTab = GOCredit.GetComponent<Animator>();
        GOConfig.GetComponent<Animator>().SetTrigger("Next");
    }

	public void LoginSelected() {
		Desativar();
		GOLogin.SetActive (true);
		ES.SetSelectedGameObject (VoltarCred.gameObject);		
	}

	public void CadastSelected() {
		Desativar();
		GOCadast.SetActive (true);
		ES.SetSelectedGameObject (VoltarCred.gameObject);		
	}

	public void VoltarMenu() {
        books[cBook].anim.SetTrigger("Close");
		GOMenu.SetActive (true);
		ES.SetSelectedGameObject (Play.gameObject);
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
