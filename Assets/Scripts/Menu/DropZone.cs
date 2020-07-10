using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public bool vertical = true;
    public bool horizontal = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null)
        {
            return;
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
    }
}
