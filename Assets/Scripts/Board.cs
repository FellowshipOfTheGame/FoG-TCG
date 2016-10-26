using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

    //Var Placing Card
    public static bool IsPlacingCard;
    public static bool PosSelected;
    public static Slot slot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void PlaceCard(CardScript card){
        card.gameObject.GetComponent<Hand>().CardReset();
        GameObject temp = Instantiate(card.gameObject) as GameObject;
        temp.transform.Rotate(Vector3.forward, 90);
        temp.transform.position = new Vector3(slot.transform.position.x, slot.transform.position.y, -0.5f);
        temp.transform.localScale = new Vector3(1.8f, 2.7f, 1);
        temp.GetComponent<Hand>().OnHand = false;
        Debug.Log("FOOOOOI!");
    }
}
