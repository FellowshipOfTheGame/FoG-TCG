using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGenerator : MonoBehaviour{

    public GameObject canvas;
    public GameObject genMenu;
    public GameObject genButton;
    GameObject menu = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject CreateMiniMenu(int number, GameObject prev, GameObject next) {
        menu = Instantiate(genMenu, canvas.transform) as GameObject;
        menu.GetComponent<MiniMenu>().prevMenu = prev;
        menu.GetComponent<MiniMenu>().nextMenu = next;
        int i;
        for (i = 0; i < number; i++) {
            GameObject button = Instantiate(genButton, menu.transform) as GameObject;
            menu.GetComponent<MiniMenu>().buttons.Add(button);
        }
        return menu;
    }
}
