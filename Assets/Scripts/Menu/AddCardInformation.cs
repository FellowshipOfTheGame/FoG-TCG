using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddCardInformation : MonoBehaviour {

    public Card info;
    public char type;
    public GameObject title;
    public Image image;
    public GameObject desc;
    public GameObject flavor;
    public GameObject cost;
    public GameObject atk;
    public GameObject hp;
    public GameObject aspects;

    // Use this for initialization
    void Start() {
        name = info["title"].ToObject<string>();
        title.GetComponent<TextMesh>().text = info["title"].ToObject<string>();
        type= info["type"].ToObject<char>();
        desc.GetComponent<TextMesh>().text = info["desc"].ToObject<string>();
        flavor.GetComponent<TextMesh>().text = info["flavor"].ToObject<string>();
        /*
        cost.GetComponent<TextMesh>().text = info["cost"].ToObject<int>().ToString();
        atk.GetComponent<TextMesh>().text = info["atk"].ToObject<int>().ToString();
        hp.GetComponent<TextMesh>().text = info["hp"].ToObject<int>().ToString();
        */

        /*
        int i, j;
        for (i = 0; i < 4; i++) {
            if (card.aspects[i] == 0)
                Destroy(aspects.transform.GetChild(i).gameObject);
        }
        for (i = 1; i <= card.aspects[0] - 1; i++) {
            transform.Find("Aspects").Find("F").SetAsLastSibling();
            GameObject newSymbol = Instantiate(transform.Find("Aspects").Find("F").gameObject, transform.Find("Aspects"));
        }
        for (i = 1; i <= card.aspects[1] - 1; i++) {
            transform.Find("Aspects").Find("W").SetAsLastSibling();
            GameObject newSymbol = Instantiate(transform.Find("Aspects").Find("W").gameObject, transform.Find("Aspects"));
        }
        for (i = 1; i <= card.aspects[2] - 1; i++) {
            transform.Find("Aspects").Find("E").SetAsLastSibling();
            GameObject newSymbol = Instantiate(transform.Find("Aspects").Find("E").gameObject, transform.Find("Aspects"));
        }
        for (i = 1; i <= card.aspects[3] - 1; i++) {
            transform.Find("Aspects").Find("A").SetAsLastSibling();
            GameObject newSymbol = Instantiate(transform.Find("Aspects").Find("A").gameObject, transform.Find("Aspects"));
        }
        */
	}
}
