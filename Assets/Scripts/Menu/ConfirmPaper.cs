using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmPaper : MonoBehaviour {
    public Animator anim, bottom;
    public GameObject bottomButton, rolha;

    public void move() {
        anim.SetTrigger("Move");
    }

    public void stop() {
        anim.SetTrigger("Stop");
    }

    public void block() {
        bottomButton.SetActive(false);
    }

    public void active() {
        bottomButton.SetActive(true);
    }

    public void moveBottom() {
        bottom.SetTrigger("Move");
    }

    public void closeBottom() {
        rolha.SetActive(false);
    }

    public void exit() {
        bottom.SetTrigger("Stop");
        Application.Quit();
    }
}
