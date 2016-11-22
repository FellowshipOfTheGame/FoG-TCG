using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeckListManager : MonoBehaviour {

    Transform currentChild;
    Transform nextChild;

    public void UpdateChildrenQuantity()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            currentChild = transform.GetChild(i);
            if (currentChild.GetComponent<AddCardInformationMinimized>().quantity == 1)
            {
                currentChild.GetComponent<AddCardInformationMinimized>().quantityText.text = " ";
            }
            else
            {
                currentChild.GetComponent<AddCardInformationMinimized>().quantityText.text = "x" + currentChild.GetComponent<AddCardInformationMinimized>().quantity;
            }
        }
    }

    public void OrderChildren()
    {
        if (transform.childCount > 0)
        {
            string currentChildName;
            string nextChildName;


            for (int j = 1; j < transform.childCount; j++)
            {
                for (int i = j; i > 0; i--)
                {
                    currentChildName = transform.GetChild(i - 1).GetComponent<AddCardInformationMinimized>().card.title;
                    nextChildName = transform.GetChild(i).GetComponent<AddCardInformationMinimized>().card.title;

                    if (string.Compare(currentChildName, nextChildName) > 0)
                    {
                        transform.GetChild(i).SetSiblingIndex(i - 1);
                    }
                }
            }
        }
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
