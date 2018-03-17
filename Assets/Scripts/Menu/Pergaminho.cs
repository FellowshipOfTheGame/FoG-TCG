using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pergaminho : MonoBehaviour {
    public GameObject button;
    public Animator anim, mainTab;
    public static Animator activeTab;

    void Start() {
        resetTab();
    }

    public void block() {
        button.SetActive(false);
    }

    public void active() {
        button.SetActive(true);
    }

    public void openTab() {
        activeTab.gameObject.SetActive(true);
        activeTab.SetTrigger("Move");
    }

    public void closeTab() {
        activeTab.SetTrigger("Move");
    }

    public void resetTab() {
        activeTab = mainTab;
    }

    public void invisible() {
        activeTab.gameObject.SetActive(false);
    }

    public void stop() {
        anim.SetTrigger("Stop");
    }

    public void stopTab() {
        activeTab.SetTrigger("Stop");
        Debug.Log(activeTab);
    }
}