using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotCanvas : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public static bool isChoosingPlace = false;

    Board board;
    Color slotColor;

	// Use this for initialization
	void Start () {
        board = this.transform.parent.parent.parent.GetComponent<Board>();
        slotColor = this.GetComponent<Image>().color;
        this.GetComponent<Image>().color = Color.clear;
    }
	
    public void OnPointerEnter(PointerEventData eventData) {
        if (isChoosingPlace) {
            this.GetComponent<Image>().color = slotColor;
            board.slot = null;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (isChoosingPlace) {
            this.GetComponent<Image>().color = Color.clear;
            board.slot = null;
        }
    }

    // Update is called once per frame
    void Update () {
      
	}
}
