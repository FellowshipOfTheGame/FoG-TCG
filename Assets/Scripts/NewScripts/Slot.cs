using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : Clickable {

	new SpriteRenderer renderer;

	void Start() {
		renderer = GetComponent<SpriteRenderer> ();
		OnPointerExit ();
	}

	public override void OnClick () {}

	public override void OnPointerEnter() {
		renderer.color = Color.white;
	}

	public override void OnPointerExit() {
		renderer.color = Color.clear;
	}
		
}
