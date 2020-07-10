using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CaptMenu : MonoBehaviour {

	bool mouseOver = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void teste(PointerEventData eventData){}

	public void OnPointerEnter(PointerEventData eventData) {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        mouseOver = false;
    }
}
