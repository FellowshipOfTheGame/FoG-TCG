using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {
    Animator anim;
    [HideInInspector] public SpriteRenderer spr;
    RuntimeAnimatorController[] allAnim;
    Card myCard;
    CardClick myCardInTable;
	// Use this for initialization
	void Start () {
        allAnim = Resources.LoadAll<RuntimeAnimatorController>("Animation/");
        myCard = this.transform.parent.GetComponent<Card>();
        myCardInTable = this.transform.parent.GetComponent<CardClick>();
        anim = this.GetComponent<Animator>();
        spr = this.GetComponent<SpriteRenderer>();
        anim.enabled = false;
        spr.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void setAnim(string animName) {
        int i = 0;
        while (allAnim[i].name != "ctrl_" + animName && i < allAnim.Length)
            i++;

        if (i != allAnim.Length) {
            anim.enabled = true;
            anim.runtimeAnimatorController = allAnim[i];
        }
    }
    public void refresh() {
        if (myCardInTable != null)
            myCardInTable.refreshCard();
    }

    public void activeImage() {
        spr.enabled = true;
    }

    public void stopAnim() {
        anim.enabled = false;
        spr.enabled = false;
    }

    public void remove() {
        myCard.Remove();
    }

    public void finish() {
        Destroy(this.gameObject);
    }
}
