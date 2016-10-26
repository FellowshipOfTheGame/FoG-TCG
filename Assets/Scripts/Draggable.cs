using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 mouseOffset;

    public Transform parentToReturnTo = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        mouseOffset = new Vector2(transform.position.x - eventData.position.x, transform.position.y - eventData.position.y);

        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        this.transform.position = eventData.position + mouseOffset;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        this.transform.SetParent(parentToReturnTo);

        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }
}
