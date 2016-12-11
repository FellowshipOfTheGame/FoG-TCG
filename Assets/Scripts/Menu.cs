using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
	public GameObject GODecks;
	public GameObject GONovoDeck;
	public GameObject GOStats;
	public GameObject GOConta;
	public GameObject GOConfig;
	public GameObject GOOpcoes;
	public GameObject GOCredit;
	public GameObject GOLogin;
	public GameObject GOCadast;

	private EventSystem ES;
	public PlaySceneMusic Music;

	void Update() {
		ES = EventSystem.current;

		if (ES.currentSelectedGameObject == null){
			if (GOMenu.activeInHierarchy)
				ES.SetSelectedGameObject (Play.gameObject);

			if (GOConfig.activeInHierarchy)
				ES.SetSelectedGameObject (VoltarConfig.gameObject);

			if (GOCredit.activeInHierarchy)
				ES.SetSelectedGameObject (VoltarCred.gameObject);
		}
	}

	public void PlaySelected() {
		Application.LoadLevel(1); // carregar jogo(trocar Game pelo nome da scene)
	}

	public void Desativar() {
		GOJogar.SetActive (false);
		GOCartas.SetActive (false);
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

	public void JogarSelected() {
		Desativar();
		GOJogar.SetActive (true);
		ES.SetSelectedGameObject (VoltarConfig.gameObject);
	}

	public void CartasSelected() {
		Desativar();
		GOCartas.SetActive (true);
		ES.SetSelectedGameObject (VoltarConfig.gameObject);
	}

	public void DecksSelected() {
		Desativar();
		GODecks.SetActive (true);
		ES.SetSelectedGameObject (VoltarConfig.gameObject);
	}

	public void NovoDeckSelected() {
		Desativar();
		GONovoDeck.SetActive (true);
		ES.SetSelectedGameObject (VoltarDecks.gameObject);
	}

	public void StatsSelected() {
		Desativar();
		GOStats.SetActive (true);
		ES.SetSelectedGameObject (VoltarConfig.gameObject);
	}

	public void ContaSelected() {
		Desativar();
		GOConta.SetActive (true);
		ES.SetSelectedGameObject (VoltarConfig.gameObject);
	}

	public void ConfigSelected() {
		Desativar();
		GOConfig.SetActive (true);
		ES.SetSelectedGameObject (VoltarCred.gameObject);		
	}

	public void OpcoesSelected() {
		Desativar();
		GOOpcoes.SetActive (true);
		ES.SetSelectedGameObject (VoltarCred.gameObject);		
	}

	public void CreditsSelected() {
		Desativar();
		GOCredit.SetActive (true);
		ES.SetSelectedGameObject (VoltarCred.gameObject);		
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
		Desativar();
		GOMenu.SetActive (true);
		ES.SetSelectedGameObject (Play.gameObject);
	}

	public void ExitSelected() {
		Application.Quit ();
	}

}
