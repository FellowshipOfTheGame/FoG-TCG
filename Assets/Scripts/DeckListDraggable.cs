using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DeckListDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Vector2 mouseOffset;
    public Transform parentToReturnTo = null;

    public Transform currentZone;


    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseOffset = new Vector2(transform.position.x - eventData.position.x, transform.position.y - eventData.position.y);
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        print(transform.parent.name);

    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position + mouseOffset;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentZone.tag == "DeckList")
        {
            this.transform.SetParent(parentToReturnTo);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}
