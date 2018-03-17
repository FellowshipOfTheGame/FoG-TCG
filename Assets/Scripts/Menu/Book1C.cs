using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book1C : MonoBehaviour {
    Button book;
    GameObject activePage, activeSide;
    public Animator anim;
    public GameObject nextPage, nextSide;
    Animator parentAnim;
    public GameObject lateral;
    public Button changeButton;

    // Use this for initialization
    void Start () {
        anim = this.GetComponent<Animator>();
        parentAnim = this.transform.parent.parent.GetComponent<Animator>();
        book = this.transform.GetChild(0).GetComponent<Button>();
    }

    public void move() {
        parentAnim.SetTrigger("Move");
    }

    public void stop() {
        parentAnim.SetTrigger("Stop");
    }

    public void block() {
        book.interactable = false;
    }

    public void active() {
        book.interactable = true;
    }

    public void letOpen() {
        anim.SetTrigger("Stay");
        book.image.raycastTarget = false;
        changeButton.interactable = false;
    }

    public void letClose() {
        anim.SetTrigger("Stay");
        book.image.raycastTarget = true;
        changeButton.interactable = true;
    }

    public void acelera() {
        lateral.SetActive(true);
        //foreach (GameObject child in lateral.transform.ge)
        parentAnim.SetTrigger("Rush");
    }

    public void liberaLateral() {
        lateral.SetActive(true);
        activeSide = nextSide;
    }

    public void ocultaLateral() {
        lateral.SetActive(false);
    }

    public void changePage() {
        if (activePage != null)
            activePage.SetActive(false);

        nextPage.SetActive(true);
        activePage = nextPage;
    }

    public void changeSide() {
        if (activeSide != null)
            activeSide.SetActive(false);
        
        nextSide.SetActive(true);
        activeSide = nextSide;
    }

    public void resetLateral() {
        lateral.transform.GetChild(0).gameObject.SetActive(false);
    }
}
