﻿using UnityEngine;
using UnityEngine.EventSystems;

public class DeckListDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Vector2 mouseOffset;
    public Transform parentToReturnTo = null;

    public Transform currentZone;
    public GameObject deckListZone;

    GameObject cardCopy;
    bool draggingCopy = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseOffset = new Vector2(transform.position.x - eventData.position.x, transform.position.y - eventData.position.y);

        if (this.GetComponent<AddCardInformationMinimized>().quantity == 1)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            parentToReturnTo = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent);
            print(transform.parent.name);
        }
        else
        { 
            draggingCopy = true;
            cardCopy = Instantiate(this.gameObject);
            RectTransform rt = cardCopy.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(180, 30);
            cardCopy.transform.SetParent(this.transform.parent.parent);
            cardCopy.GetComponent<CanvasGroup>().blocksRaycasts = false;
            currentZone = deckListZone.transform;
            cardCopy.GetComponent<AddCardInformationMinimized>().quantity = 1;

            this.GetComponent<AddCardInformationMinimized>().quantity--;
            this.transform.parent.GetComponent<DeckListManager>().UpdateChildrenQuantity();
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingCopy)
        {
            cardCopy.transform.position = eventData.position + mouseOffset;
        }
        else {
            this.transform.position = eventData.position + mouseOffset;
        }
        print(currentZone);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentZone.tag == "DeckList")
        {
            if (draggingCopy)
            {
                this.GetComponent<AddCardInformationMinimized>().quantity++;
                currentZone.GetComponent<DeckListManager>().UpdateChildrenQuantity();
                Destroy(cardCopy);
                draggingCopy = false;
            }
            else
            {
                this.transform.SetParent(parentToReturnTo);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
        else
        {
            if (draggingCopy)
            {
                Destroy(cardCopy);
                draggingCopy = false;
            }
            else {
                Destroy(this.gameObject);
            }
        }


    }
}
