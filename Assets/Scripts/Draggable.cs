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

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        mouseOffset = new Vector2(transform.position.x - eventData.position.x, transform.position.y - eventData.position.y);

        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        this.transform.position = eventData.position + mouseOffset;

        if(placeholder.transform.parent != placeholderParent)
        {
            placeholder.transform.parent = placeholderParent;
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
        Debug.Log("OnEndDrag");
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(placeholder);

    }
}
