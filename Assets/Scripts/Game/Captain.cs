using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Captain : Clickable {

    public bool canMove = true;
    public bool canBuy = true;
    public bool canGenerate = true;
    public int pos;
    [Space(5)]
    public float[] posDefaults;
    float originalPos;
    Board board;
    bool dragging = false;
    Vector3 diff;
    public AnimationManager[] anims;
    SpriteRenderer spr;
    public Color inColor, offColor;

    public GameObject menu;
	// Use this for initialization

    [HideInInspector] public Player player;
	void Start () {
        board = GameObject.FindObjectOfType<Board>() as Board;
        spr = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (dragging) {
            Dragging();
            if (Input.GetMouseButtonUp(0))
                Dropping();
        }
	}

    public override void OnPointerEnter() {
        if (canMove || canGenerate || canBuy)
            spr.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
    }

    public override void OnPointerExit() {
        if (canMove || canGenerate || canBuy)
            spr.color = Color.clear;
    }

    public void block() {
        spr.color = Color.clear;
        canBuy = false;
        canGenerate = false;
        canMove = false;
    }

    public override void OnClick(int mouseButton) {
        if (mouseButton == 0 && canMove) {
            originalPos = this.transform.position.x;
            dragging = true;
            diff = this.transform.position - board.mousePosition;
        }else if(mouseButton == 1 && canGenerate && canBuy){
            Command();
        }
    }

    public void Command(){
        board.ray.enabled = false;
        menu.SetActive(true);
    }

    public void AddAspect(int index) {
        player.aspects[index]++;
        player.capt.canBuy = false;
        player.capt.canGenerate = false;
        player.capt.canMove = false;

        if (index == 0) player.display.notify("Fire", 1, 0f);
        if (index == 1) player.display.notify("Water", 1, 0f);
        if (index == 2) player.display.notify("Earth", 1, 0f);

        HideMenu();
    }

    public void HideMenu(){
        board.ray.enabled = true;
        //spr.color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        menu.SetActive(false);
    }

    public void BuyAnCard() {
        player.PickUpCard();
        player.capt.canBuy = false;
        player.capt.canGenerate = false;
        player.capt.canMove = false;

        HideMenu();
    }

    void Dragging() {
        float[] limit = new float[2];
        limit[0] = posDefaults[0];
        limit[1] = posDefaults[2];

        if (pos == 1)
            limit[1] = posDefaults[1];
        
        if (pos == 3)
            limit[0] = posDefaults[1];

        if ((board.mousePosition + diff).x < limit[0]) {
            this.transform.position = new Vector3(limit[0], this.transform.position.y, this.transform.position.z);
        }else if ((board.mousePosition + diff).x > limit[1]) {
            this.transform.position = new Vector3(limit[1], this.transform.position.y, this.transform.position.z);
        }else {
            this.transform.position = new Vector3((board.mousePosition + diff).x, this.transform.position.y, this.transform.position.z);
        }
    }

    void Dropping() {
        if (this.transform.position.x - originalPos < -(posDefaults[1] - posDefaults[0]) / 2) {
            pos--;
            canGenerate = false;
            canBuy = false;
        } else if (this.transform.position.x - originalPos > (posDefaults[1] - posDefaults[0]) / 2) {
            pos++;
            canGenerate = false;
            canBuy = false;
        }
        this.transform.position = new Vector3(posDefaults[pos - 1], this.transform.position.y, this.transform.position.z);
        dragging = false;
        //canMove = false;
    }

    public void Reset() {
        pos = 2;
        this.transform.position = new Vector3(posDefaults[1], this.transform.position.y, this.transform.position.z);
    }

}
