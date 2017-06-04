using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddCardInformation : MonoBehaviour {

    public CardInformation card;
    public Text title;
    public Image image;
    public Text desc;
    public Text flavor;
    public Text cost;
    public Text atk;
    public Text hp;
    public GameObject aspects;

    // Use this for initialization
    void Start() {
        name = card.title;
        title.text = card.title;
        desc.text = card.desc;
        flavor.text = card.flavor;
        cost.text = card.cost.ToString();
        atk.text = card.ATK.ToString();
        hp.text = card.HP.ToString();

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

        switch (card.type) {
            case 'c':
                gameObject.AddComponent<CreatureCard>();
                gameObject.GetComponent<Image>().color = new Color(0.84f, 0.88f, 0.62f, gameObject.GetComponent<Image>().color.a);
                gameObject.GetComponent<CardInTable>().ATK = card.ATK;
                gameObject.GetComponent<CardInTable>().HP = card.HP;
                break;
            case 't':
                gameObject.AddComponent<TerrainCard>();
                gameObject.GetComponent<Image>().color = new Color(0.79f, 0.75f, 1.00f, gameObject.GetComponent<Image>().color.a);
                gameObject.GetComponent<CardInTable>().manaToGive = card.manaToGive;
                for (i = 0; i < 4; i++)
                    gameObject.GetComponent<CardInTable>().aspectsToGive[i] = card.aspectsToGive[i];

                break;
            case 'a':
                gameObject.GetComponent<Image>().color = new Color(0.91f, 0.91f, 0.91f, gameObject.GetComponent<Image>().color.a);
                for (i = 0; i < card.aspectsToGive.Length; i++)
                    gameObject.GetComponent<CardInTable>().aspectsToGive[i] = card.aspectsToGive[i];

                break;
        }
	}
}
