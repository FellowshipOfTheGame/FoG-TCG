using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public DeckListManager deckListManager;

    public Canvas canvas;

    GameObject cardCopy;
    Vector2 mouseOffset;
    public Transform currentZone;
    public GameObject deckListZone;
    public GameObject minimizedCard;

    public GameObject collectionZone;
    public GameObject deckList;

    bool dragging = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (deckListManager.GetDeckSize() < 30)
        {
            if (deckListManager.CheckCardCount(this.name))
            {
                mouseOffset = new Vector2(transform.position.x - eventData.position.x, transform.position.y - eventData.position.y);
                cardCopy = Instantiate(this.gameObject);
                RectTransform rt = cardCopy.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(120, 180);
                cardCopy.transform.SetParent(canvas.transform);
                //cardCopy.transform.SetParent(transform.parent.parent.parent.parent.parent.parent.transform);
                cardCopy.transform.localScale = Vector3.one;
                currentZone = collectionZone.transform;
                cardCopy.GetComponent<CanvasGroup>().blocksRaycasts = false;

                dragging = true;
            }
            else
            {
                print("You have too many of that card in your deck!");
                dragging = false;
            }
        }
        else
        {
            print("You have too many cards in your deck!");
            dragging = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            cardCopy.transform.position = eventData.position + mouseOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            if (currentZone.tag == "DeckList")
            {
                GameObject mc = (GameObject)Instantiate(minimizedCard, currentZone);
                mc.GetComponent<AddCardInformationMinimized>().card = this.GetComponent<AddCardInformation>().card;
                mc.GetComponent<DeckListDraggable>().deckListZone = currentZone.gameObject;
                mc.GetComponent<DeckListDraggable>().canvas = canvas;
                mc.transform.localScale = Vector3.one;
                currentZone.GetComponent<DeckListManager>().deckSize++;

                currentZone.GetComponent<DeckListManager>().OrderChildren();
                currentZone.GetComponent<DeckListManager>().CheckForMultiples();
            }
            dragging = false;
            cardCopy.GetComponent<CanvasGroup>().blocksRaycasts = true;
            Destroy(cardCopy);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!dragging)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (deckListManager.GetDeckSize() < 30)
                {
                    if (deckListManager.CheckCardCount(this.name))
                    {
                        GameObject mc = (GameObject)Instantiate(minimizedCard, deckListZone.transform);
                        mc.GetComponent<AddCardInformationMinimized>().card = this.GetComponent<AddCardInformation>().card;
                        mc.GetComponent<DeckListDraggable>().deckListZone = deckListZone;
                        mc.GetComponent<DeckListDraggable>().canvas = canvas;
                        mc.transform.localScale = Vector3.one;

                        deckListZone.GetComponent<DeckListManager>().deckSize++;

                        deckListZone.GetComponent<DeckListManager>().OrderChildren();
                        deckListZone.GetComponent<DeckListManager>().CheckForMultiples();
                    }
                    else
                    {
                        print("You have too many of that card in your deck!");
                    }
                }
                else
                {
                    print("You have too many cards in your deck!");
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                print("MAIS INFORMAÇÕES DESSA CARTA");
            }
        }
    }
}
