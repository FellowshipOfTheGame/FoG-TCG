using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStatics : MonoBehaviour {

	public Text estatisticas;

	public void setTexto () {
		
		estatisticas.text = "";

		estatisticas.text += "Vitórias: " + GameData.playerInfo.vit + "\n";
		estatisticas.text += "Comandante com mais vitórias: " + GameData.playerInfo.com_vit + "\n";
		estatisticas.text += "Derrotas: " + GameData.playerInfo.derr + "\n";
		estatisticas.text += "Comandante com mais derrotas: " + GameData.playerInfo.com_derr + "\n";

		estatisticas.text += "\n";
		estatisticas.text += "Dinheiro máximo: " + GameData.playerInfo.max_money + "\n";
		estatisticas.text += "Dinheiro acumulado: " + GameData.playerInfo.ac_money + "\n";
		estatisticas.text += "Carta Favorita: " + GameData.playerInfo.fav_card + "\n";
		estatisticas.text += "Horas jogadas: " + GameData.playerInfo.time + "\n";

		estatisticas.text += "\n";
		estatisticas.text += "Cartas Desbloquadas: " + GameData.playerInfo.unlocked + "\n";
		estatisticas.text += "Cartas Compradas: " + GameData.playerInfo.bought + "\n";

		estatisticas.text += "\n";
		estatisticas.text += "Criaturas Compradas: " + GameData.playerInfo.cria_bought + "\n";
		estatisticas.text += "Criaturas Destruídas: " + GameData.playerInfo.cria_destr + "\n";
		estatisticas.text += "Terrenos Comprados: " + GameData.playerInfo.terr_bought + "\n";
		estatisticas.text += "Terrenos Destruídos: " + GameData.playerInfo.terr_destr + "\n";
		estatisticas.text += "Magias Comprados: " + GameData.playerInfo.magia_bought + "\n";
		estatisticas.text += "Magias Usadas: " + GameData.playerInfo.magia_destr;
	}

}
