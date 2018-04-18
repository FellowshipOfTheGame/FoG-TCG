using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MiniMenu : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {
    public static List<List<MiniMenu>> menuLayers = new List<List<MiniMenu>>();
    public GameObject prevMenu = null;
    public GameObject nextMenu = null;
    public List<GameObject> buttons = new List<GameObject>();
    int mouseOver = 0, layer;

	// Use this for initialization
	void Start () {
        if (prevMenu != null)
            layer = prevMenu.GetComponent<MiniMenu>().layer + 1;
        else
            layer = 0;

        if (menuLayers.Count <= layer) 
            menuLayers.Add(new List<MiniMenu>());

        menuLayers[layer].Add(this.GetComponent<MiniMenu>());
    }
	
	// Update is called once per frame
	void Update () {
        if (mouseOver == -1) {
            if (nextMenu == null)
                DestroyMenu(false);
            else {
                if(nextMenu.GetComponent<MiniMenu>().mouseOver == 0) {
                    nextMenu.GetComponent<MiniMenu>().DestroyMenu(false);
                }
            }
        }
	}

    public void OnPointerEnter(PointerEventData eventData) {
        mouseOver = 1;
    }

    public void OnPointerExit(PointerEventData eventData) {
        mouseOver = -1;
    }

    public void DestroyMenu(bool clear) {
        if (menuLayers.Count > layer + 1) {
            for (int j = 0; j < menuLayers[layer + 1].Count; j++)
                    menuLayers[layer + 1][j].GetComponent<MiniMenu>().DestroyMenu(false);
        }

        if (prevMenu != null) {
            if (clear)
                prevMenu.GetComponent<MiniMenu>().DestroyMenu(true);
            else
                prevMenu.GetComponent<MiniMenu>().nextMenu = null;
        }
        Destroy(this.gameObject);
    }
}
