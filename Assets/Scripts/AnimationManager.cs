using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {
    Animator anim, modifierAnim;

    public SpriteRenderer modifierSlot;

    public Sprite modifier;
    [HideInInspector] public SpriteRenderer spr;
    RuntimeAnimatorController[] allAnim;

    Sprite[] allModifiers;
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
        allModifiers = Resources.LoadAll<Sprite>("Effects/");
        myCard = this.transform.parent.GetComponent<Card>();
        myCardInTable = this.transform.parent.GetComponent<CardClick>();
        anim = this.GetComponent<Animator>();
        spr = this.GetComponent<SpriteRenderer>();

        if (modifierSlot != null) modifierAnim = modifierSlot.transform.parent.GetComponent<Animator>();
    }

    public void setAnim(string animName) {
        int i = 0;
        while (i < allAnim.Length && allAnim[i].name != "ctrl_" + animName)
            i++;

        if (i != allAnim.Length) {
            anim.enabled = true;
            if (anim.runtimeAnimatorController.name != allAnim[i].name)
                anim.runtimeAnimatorController = allAnim[i];
            else
                anim.Rebind();
        }else{
            spr.enabled = false;
        }
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
        spr.enabled = false;
        if(myCard != null && myCardInTable != null && !myCardInTable.inHand && !myCard.canAttack && !myCard.canCast){
            setAnim("sleep");
        }

        if (oneShot)
            Destroy(this.gameObject);
    }

    public void remove() {
        myCard.Remove();
        oneShot = true;
    }

    public void finish() {
        Destroy(this.gameObject);
    }

    public void addModifier(string modifierName, float delay){
        int i = 0;
        while (i < allModifiers.Length && allModifiers[i].name != "fx_" + modifierName)
            i++;

        if (i != allModifiers.Length) {
            modifier = allModifiers[i];
        }else{
            modifier = null;
        }

        CancelInvoke();
        Invoke("ShowModifier", delay);
    }

    void ShowModifier(){
        modifierSlot.sprite = modifier;
        if(modifier != null){
            name = char.ToUpper(modifier.name[3]) + modifier.name.Substring(4);
        }else{
            name = "Anim";
        }

        modifierAnim.SetTrigger("show");
    }

    public void removeModifier(string name, float delay){
        modifier = null;

        CancelInvoke();
        Invoke("ShowModifier", delay);
    }
}
