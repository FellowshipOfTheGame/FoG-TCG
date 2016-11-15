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
            StartCoroutine(DeckListUpdateDelay());
        }

        cardCopy.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(cardCopy);
    }

    public void OrderChildren(Transform currentZone)
    {
        if (currentZone.transform.childCount > 0)
        {
            string currentChildName;
            string nextChildName;


            for(int j = 1; j < currentZone.transform.childCount; j++)
            {
                for (int i = j; i > 0; i--)
                {
                    currentChildName = currentZone.transform.GetChild(i - 1).GetComponent<AddCardInformationMinimized>().card.title;
                    nextChildName = currentZone.transform.GetChild(i).GetComponent<AddCardInformationMinimized>().card.title;

                    if (string.Compare(currentChildName, nextChildName) > 0)
                    {
                        currentZone.transform.GetChild(i).SetSiblingIndex(i-1);
                    }
                }
            }
        }
    }

    IEnumerator DeckListUpdateDelay()
    {

        //returning 0 will make it wait 1 frame
        yield return 0;

        currentZone.GetComponent<DeckListManager>().CheckForMultiples();
    }
}
