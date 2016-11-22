using UnityEngine;
using System.Collections;

public class DeckListManager : MonoBehaviour {

    Transform currentChild;
    Transform nextChild;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void CheckForMultiples()
    {

        for(int i = 0; i < transform.childCount-1; i++)
        {

            if(string.Compare(transform.GetChild(i).GetComponent<AddCardInformationMinimized>().card.title, transform.GetChild(i+1).GetComponent<AddCardInformationMinimized>().card.title) == 0)
            {
                transform.GetChild(i).GetComponent<AddCardInformationMinimized>().quantity++;
                transform.GetChild(i).GetComponent<AddCardInformationMinimized>().quantityText.text = "x" + transform.GetChild(i).GetComponent<AddCardInformationMinimized>().quantity;

                Destroy(transform.GetChild(i + 1).gameObject);


            }
            
        }

    }
}
