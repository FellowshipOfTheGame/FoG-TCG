﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler {

	public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop to " + gameObject.name);
    }
}
