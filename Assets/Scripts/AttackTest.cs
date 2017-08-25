using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackTest : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData point)
    {
        Card self = gameObject.GetComponent<Card>();
        
        self.Attack();
    }
}
