using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnSignal : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Board.CurrPlayer == 1)
            transform.gameObject.GetComponent<Image>().color = Color.blue;
        else
            transform.gameObject.GetComponent<Image>().color = Color.black;
    }
}
