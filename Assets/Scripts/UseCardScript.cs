using UnityEngine;
using System.Collections;

public class UseCardScript : MonoBehaviour {

    public static CardScript thisCard;
    public static bool IsSelectedCard;
    public static bool IsChoosingCard;

	// Use this for initialization
	void Start () {
        IsSelectedCard = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (IsChoosingCard == true && Input.GetMouseButtonDown(0)){
            if (IsSelectedCard == true){
                if (Board.IsPlacingCard == true){
                    if (Board.PosSelected == true){
                        Board.PlaceCard(thisCard);

                        IsSelectedCard = false;
                        IsChoosingCard = false;
                    }
                }else
                    Board.IsPlacingCard = true;
            }
        }
	}

    void OnMouseDown(){
        IsChoosingCard = true;
        Debug.Log("Pega a Carta!");
    }
}
