using UnityEngine;
using UnityEngine.EventSystems;

public class DeckListDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    public Canvas canvas;

    Vector2 mouseOffset;
    public Transform parentToReturnTo = null;

    public Transform currentZone;
    public GameObject deckListZone;

    GameObject cardCopy;
    bool draggingCopy = false;

    DeckListManager deckListManager;

    void Start(){
        deckListManager = this.transform.parent.GetComponent<DeckListManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseOffset = new Vector2(transform.position.x - eventData.position.x, transform.position.y - eventData.position.y);

        if (this.GetComponent<AddCardInformationMinimized>().quantity == 1)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            parentToReturnTo = this.transform.parent;
            this.transform.SetParent(canvas.transform);
        }
        else
        { 
            draggingCopy = true;
            cardCopy = Instantiate(this.gameObject);
            RectTransform rt = cardCopy.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(274.5f, 64.0f);
            cardCopy.transform.SetParent(canvas.transform);
            cardCopy.transform.localScale = Vector3.one;
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

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentZone.tag == "DeckList")
        {
            if (draggingCopy)
            {
                this.GetComponent<AddCardInformationMinimized>().quantity++;
                GameManager.instance.ValidateDeck();
                currentZone.GetComponent<DeckListManager>().UpdateChildrenQuantity();
                Destroy(cardCopy);
                draggingCopy = false;
            }
            else
            {
                this.transform.SetParent(parentToReturnTo);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                currentZone.GetComponent<DeckListManager>().OrderChildren();
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
            GameManager.instance.ValidateDeck();
            deckListZone.GetComponent<DeckListManager>().deckSize--;
		}
		deckListZone.GetComponent<DeckListManager> ().setDeckSizeLabel ();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left){
            if(this.GetComponent<AddCardInformationMinimized>().quantity == 1){
                Destroy(this.gameObject);
            }else{
                this.GetComponent<AddCardInformationMinimized>().quantity--;
                deckListZone.GetComponent<DeckListManager>().UpdateChildrenQuantity();
            }
            deckListManager.deckSize--;
        }else if (eventData.button == PointerEventData.InputButton.Right){
            if (deckListManager.GetDeckSize() < 30 && deckListManager.CheckCardCount(this.name)) {
                    this.GetComponent<AddCardInformationMinimized>().quantity++;
                    deckListManager.deckSize++;
                    deckListZone.GetComponent<DeckListManager>().UpdateChildrenQuantity();
                } else {
                    print("You have too many of that card in your deck!");
            
                }
        }

        GameManager.instance.ValidateDeck();
        deckListManager.OrderChildren();
        deckListManager.CheckForMultiples();
        deckListZone.GetComponent<DeckListManager>().setDeckSizeLabel();
    }
}
