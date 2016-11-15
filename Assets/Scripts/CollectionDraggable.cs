using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    GameObject cardCopy;
    Vector2 mouseOffset;
    public Transform currentZone;
    public GameObject minimizedCard;

    public GameObject collectionZone;

    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseOffset = new Vector2(transform.position.x - eventData.position.x, transform.position.y - eventData.position.y);
        cardCopy = Instantiate(this.gameObject);
        RectTransform rt = cardCopy.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(120, 180);
        cardCopy.transform.SetParent(this.transform.parent.parent);
        currentZone = collectionZone.transform;

        cardCopy.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        cardCopy.transform.position = eventData.position + mouseOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(currentZone.tag == "DeckList")
        {
            GameObject mc = (GameObject)Instantiate(minimizedCard, currentZone);
            mc.GetComponent<AddCardInformationMinimized>().card = this.GetComponent<AddCardInformation>().card;

            OrderChildren(currentZone);
        }

        cardCopy.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(cardCopy);
    }

    public void OrderChildren(Transform currentZone)
    {
        if (currentZone.transform.childCount > 0)
        {
            Transform currentChild;
            Transform nextChild;
            currentChild = currentZone.transform.GetChild(0);
            for (int i = 1; i < currentZone.transform.childCount; i++)
            {
                nextChild = currentZone.transform.GetChild(i);
                print(currentChild + "  " + nextChild);
                if (string.Compare(currentChild.GetComponent<AddCardInformationMinimized>().card.title, nextChild.GetComponent<AddCardInformationMinimized>().card.title) > 0)
                {
                    currentChild.SetSiblingIndex(i);
                }

                currentChild = nextChild;

            }
        }
    }
}
