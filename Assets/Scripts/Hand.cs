using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
    public bool OnHand = true;
    
    void OnMouseEnter(){
        if (OnHand == true && UseCardScript.IsChoosingCard == true && UseCardScript.IsSelectedCard == false)
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
    }


	
    void OnMouseOver(){
        if (OnHand == true && UseCardScript.IsChoosingCard == true && UseCardScript.IsSelectedCard == false){
            if (Input.GetMouseButtonDown(0)){
                UseCardScript.IsSelectedCard = true;
                Debug.Log("Manda pro Campo!");
            }
            UseCardScript.thisCard = transform.GetComponent<CardScript>();
        }
            
    }

    public void CardReset(){
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
        /*
        if(OnHand==false)
            Destroy(transform.gameObject);      
        */       
    }

    void OnMouseExit(){
        if (OnHand == true && UseCardScript.IsChoosingCard == true && UseCardScript.IsSelectedCard == false)
            CardReset();   
    }
}
