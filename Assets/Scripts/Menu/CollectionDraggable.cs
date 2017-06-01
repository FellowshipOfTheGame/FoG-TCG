using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public static DeckListManager deckListManager;

    public static Canvas canvas;

    GameObject cardCopy;
    Vector2 mouseOffset;
    public Transform currentZone;
    public GameObject deckListZone;
    public GameObject minimizedCard;

    public static GameObject collectionZone;
    public static bool canDrag = false;
    public GameObject deckList;

    bool dragging = false;

    public void OnBeginDrag(PointerEventData eventData){
        if (canDrag) {
            if (deckListManager.GetDeckSize() < 30) {
                if (deckListManager.CheckCardCount(this.name)) {
                    mouseOffset = new Vector2(transform.position.x - eventData.position.x, transform.position.y - eventData.position.y);
                    cardCopy = Instantiate(this.gameObject);
                    RectTransform rt = cardCopy.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(90, 135);
                    cardCopy.transform.SetParent(canvas.transform);
                    cardCopy.transform.localScale = Vector3.one;
                    currentZone = collectionZone.transform;
                    cardCopy.GetComponent<CanvasGroup>().blocksRaycasts = false;

                    dragging = true;
                } else {
                    print("You have too many of that card in your deck!");
                    dragging = false;
                }
            } else {
                print("You have too many cards in your deck!");
                dragging = false;
            }
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
            if (currentZone.tag == "DeckList"){
                GameObject mc = Instantiate(minimizedCard, currentZone) as GameObject;
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

    public void OnPointerClick(PointerEventData eventData){
        if (GameManager.currScene == 'm') {
            if (canDrag) {
                if (!dragging) {
                    if (eventData.button == PointerEventData.InputButton.Left) {
                        if (deckListManager.GetDeckSize() < 30) {
                            if (deckListManager.CheckCardCount(this.name)) {
                                GameObject mc = (GameObject)Instantiate(minimizedCard, deckListZone.transform);
                                mc.GetComponent<AddCardInformationMinimized>().card = this.GetComponent<AddCardInformation>().card;
                                mc.GetComponent<DeckListDraggable>().deckListZone = deckListZone;
                                mc.GetComponent<DeckListDraggable>().canvas = canvas;
                                mc.transform.localScale = Vector3.one;

                                deckListZone.GetComponent<DeckListManager>().deckSize++;

                                deckListZone.GetComponent<DeckListManager>().OrderChildren();
                                deckListZone.GetComponent<DeckListManager>().CheckForMultiples();
                            } else {
                                print("You have too many of that card in your deck!");
                            }
                        } else {
                            print("You have too many cards in your deck!");
                        }
                    } else if (eventData.button == PointerEventData.InputButton.Right) {
                        print("MAIS INFORMAÇÕES DESSA CARTA");
                    }
                }
            } else {
                Transform cardInfo = transform.parent.parent.parent.parent.parent.Find("MenuLateral").Find("Cards");
                cardInfo.GetChild(1).GetComponent<Text>().text = "Nome: " + transform.GetComponent<AddCardInformation>().card.title;
                switch (transform.GetComponent<AddCardInformation>().card.type) {
                    case 'c':
                        cardInfo.GetChild(1).GetComponent<Text>().text += "\nTipo: Criatura";
                        break;

                    case 't':
                        cardInfo.GetChild(1).GetComponent<Text>().text += "\nTipo: Terreno";
                        break;

                    case 'a':
                        cardInfo.GetChild(1).GetComponent<Text>().text += "\nTipo: Atmosfera";
                        break;
                }
                cardInfo.GetChild(1).GetComponent<Text>().text += "\nDescrição";
                cardInfo.GetChild(2).GetComponent<Text>().text = transform.GetComponent<AddCardInformation>().card.desc;
            }
        }
    }
}
