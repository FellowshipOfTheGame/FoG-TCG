using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MiniMenu : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {

    public GameObject prevMenu = null;
    public GameObject nextMenu = null;
    public List<GameObject> buttons = new List<GameObject>();
    int mouseOver = 0;

	// Use this for initialization
	void Start () {
		
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
        if (prevMenu != null) {
            if (clear)
                prevMenu.GetComponent<MiniMenu>().DestroyMenu(true);
            else
                prevMenu.GetComponent<MiniMenu>().nextMenu = null;
        }
        Destroy(this.gameObject);
    }
}
