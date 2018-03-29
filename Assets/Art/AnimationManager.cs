using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {
    Animator anim;
    [HideInInspector] public SpriteRenderer spr;
    RuntimeAnimatorController[] allAnim;
    Card myCard;
    [HideInInspector] public bool oneShot = false;
    CardClick myCardInTable;
	// Use this for initialization
	void Start () {
        LoadAll();
        spr.enabled = false;
        if (!oneShot)
            anim.enabled = false;
        
    }
	
	public void LoadAll() {
        allAnim = Resources.LoadAll<RuntimeAnimatorController>("Animation/");
        myCard = this.transform.parent.GetComponent<Card>();
        myCardInTable = this.transform.parent.GetComponent<CardClick>();
        anim = this.GetComponent<Animator>();
        spr = this.GetComponent<SpriteRenderer>();
    }

    public void setAnim(string animName) {
        int i = 0;

        while (allAnim[i].name != "ctrl_" + animName && i < allAnim.Length)
            i++;

        if (i != allAnim.Length) {
            anim.enabled = true;
            anim.runtimeAnimatorController = allAnim[i];
        }

        if (oneShot)
            Debug.Log(anim.enabled);
    }
    public void refresh() {
        if (myCardInTable != null)
            myCardInTable.refreshCard();
    }

    public void activeImage() {
        spr.enabled = true;
    }

    public void unParent() {
        this.transform.SetParent(this.transform.parent.parent);
    }

    public void stopAnim() {
        anim.enabled = false;
        spr.enabled = false;
        if (oneShot)
            finish();
    }

    public void remove() {
        myCard.Remove();
    }

    public void finish() {
        Destroy(this.gameObject);
    }
}
