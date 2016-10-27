using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 mouseOffset;

    public Transform parentToReturnTo = null;
    public Transform placeholderParent = null;

    public GameObject placeholder = null;

    public GameObject minimizedCard;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //if using placeholder
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        
        
        //to follow the mouse wherever in the card area the player clicks
        mouseOffset = new Vector2(transform.position.x - eventData.position.x, transform.position.y - eventData.position.y);

        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position + mouseOffset;

        if(placeholder.transform.parent != placeholderParent)
        {
            placeholder.transform.SetParent(placeholderParent);
        }

        int newSiblingIndex = placeholderParent.childCount;

        for(int i=0; i < placeholderParent.childCount; i++)
        {
            if (placeholderParent.GetComponent<DropZone>().vertical == true)
            {
                if (this.transform.position.y > placeholderParent.GetChild(i).position.y)
                {

                    newSiblingIndex = i;

                    if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    {
                        newSiblingIndex--;
                    }
                    break;
                }
            }
            if (placeholderParent.GetComponent<DropZone>().horizontal == true)
            {
                if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
                {

                    newSiblingIndex = i;

                    if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    {
                        newSiblingIndex--;
                    }
                    break;
                }
            }
        }

        placeholder.transform.SetSiblingIndex(newSiblingIndex);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.tag == "Card" && placeholderParent.tag == "DeckList")
        {
            GameObject mc = (GameObject)Instantiate(minimizedCard, placeholderParent);
            mc.GetComponent<AddCardInformationMinimized>().card = this.GetComponent<AddCardInformation>().card;
        }
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        Destroy(placeholder);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        print(this.tag + "  " + placeholderParent.tag);
        if (this.tag == "MinimizedCard" && placeholderParent.tag != "DeckList")
        {
            print("banana");
            Destroy(this.gameObject);
        }
    }
}
