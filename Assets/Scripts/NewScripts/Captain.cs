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

	// Use this for initialization
	void Start () {
        board = GameObject.FindObjectOfType<Board>() as Board;
	}
	
	// Update is called once per frame
	void Update () {
        if (dragging) {
            Dragging();
            if (Input.GetMouseButtonUp(0))
                Dropping();
        }
	}

    public override void OnClick(int mouseButton) {
        if (mouseButton == 0 && canMove) {
            originalPos = this.transform.position.x;
            dragging = true;
            diff = this.transform.position - board.mousePosition;
        }else if(mouseButton == 1 && canGenerate && canBuy){
            GameObject menu = this.GetComponent<MenuGenerator>().CreateMiniMenu(2, null, null);
            menu.transform.position = Input.mousePosition;
            Player player = board.players[board.currPlayer - 1];
            menu.GetComponent<MiniMenu>().buttons[0].GetComponent<Button>().onClick.AddListener(() => { AddAspectMenu(menu.transform.position + new Vector3(120.0f,0.0f,0.0f), menu); });
            menu.GetComponent<MiniMenu>().buttons[0].transform.GetChild(0).GetComponent<Text>().text = "Create Aspect";
            menu.GetComponent<MiniMenu>().buttons[1].GetComponent<Button>().onClick.AddListener(() => { BuyAnCard(player, menu); });
            menu.GetComponent<MiniMenu>().buttons[1].transform.GetChild(0).GetComponent<Text>().text = "Buy Another Card";
        }
    }

    void AddAspectMenu(Vector3 position, GameObject mainMenu) {
        GameObject menuAux = this.GetComponent<MenuGenerator>().CreateMiniMenu(4, mainMenu, null);
        mainMenu.GetComponent<MiniMenu>().nextMenu = menuAux;
        menuAux.transform.position = position;
        Player player = board.players[board.currPlayer - 1];
        menuAux.GetComponent<MiniMenu>().buttons[0].GetComponent<Button>().onClick.AddListener(() => { AddAspect(0, player, menuAux); });
        menuAux.GetComponent<MiniMenu>().buttons[0].transform.GetChild(0).GetComponent<Text>().text = "Fire";
        menuAux.GetComponent<MiniMenu>().buttons[1].GetComponent<Button>().onClick.AddListener(() => { AddAspect(1, player, menuAux); });
        menuAux.GetComponent<MiniMenu>().buttons[1].transform.GetChild(0).GetComponent<Text>().text = "Water";
        menuAux.GetComponent<MiniMenu>().buttons[2].GetComponent<Button>().onClick.AddListener(() => { AddAspect(2, player, menuAux); });
        menuAux.GetComponent<MiniMenu>().buttons[2].transform.GetChild(0).GetComponent<Text>().text = "Earth";
        menuAux.GetComponent<MiniMenu>().buttons[3].GetComponent<Button>().onClick.AddListener(() => { AddAspect(3, player, menuAux); });
        menuAux.GetComponent<MiniMenu>().buttons[3].transform.GetChild(0).GetComponent<Text>().text = "Air";
    }

    void AddAspect(int index, Player player, GameObject menu) {
        player.aspects[index]++;
        player.capt.canBuy = false;
        player.capt.canGenerate = false;
        player.capt.canMove = false;
        menu.GetComponent<MiniMenu>().DestroyMenu(true);
    }

    void BuyAnCard(Player player, GameObject menu) {
        player.PickUpCard();
        player.capt.canBuy = false;
        player.capt.canGenerate = false;
        player.capt.canMove = false;
        menu.GetComponent<MiniMenu>().DestroyMenu(true);
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
            board.players[board.currPlayer - 1].canPlay = false;
        } else if (this.transform.position.x - originalPos > (posDefaults[1] - posDefaults[0]) / 2) {
            pos++;
            canGenerate = false;
            canBuy = false;
            board.players[board.currPlayer - 1].canPlay = false;
        }
        this.transform.position = new Vector3(posDefaults[pos - 1], this.transform.position.y, this.transform.position.z);
        dragging = false;
        //canMove = false;
    }
}
