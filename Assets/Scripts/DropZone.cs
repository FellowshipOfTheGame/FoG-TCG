using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public bool vertical = true;
    public bool horizontal = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null)
        {
            return;
        }
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.placeholderParent = this.transform;
        }
        CollectionDraggable cd = eventData.pointerDrag.GetComponent<CollectionDraggable>();
        if(cd != null)
        {
            cd.currentZone = this.transform;
        }
        DeckListDraggable dld = eventData.pointerDrag.GetComponent<DeckListDraggable>();
        if (dld != null)
        {
            dld.currentZone = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parentToReturnTo;
        }
    }

	public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && this.tag != "DeckList")
        {
            d.parentToReturnTo = this.transform;
        }
    }
}
