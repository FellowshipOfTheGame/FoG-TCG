using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddCardInformationSemCanvas : MonoBehaviour {

    public Card info;
    public GameObject title;
    public GameObject image;
    public GameObject desc;
    public GameObject flavor;
    public GameObject cost;
    public GameObject atk;
    public GameObject hp;
    public GameObject aspects;

    [Space(10)]
    public Sprite fire;
    public Sprite water;
    public Sprite earth;
    public Sprite air;
    public float spacament;

    // Use this for initialization
    void Start() {
        //add information in Card class
        this.GetComponent<Card>().cost = info["cost"].ToObject<int>();
        this.GetComponent<Card>().type = info["type"].ToObject<char>();
        this.GetComponent<Card>().aspects = info["aspects"].ToObject<int[]>();
        if(this.GetComponent<Card>().type == 'c') {
            this.GetComponent<Card>().atk = info["atk"].ToObject<int>();
            atk.GetComponent<TextMesh>().text = info["atk"].ToObject<int>().ToString();
            this.GetComponent<Card>().hp = info["hp"].ToObject<int>();
            hp.GetComponent<TextMesh>().text = info["hp"].ToObject<int>().ToString();

        }
        

        //draw infomation
        name = info["title"].ToObject<string>();
        title.GetComponent<TextMesh>().text = info["title"].ToObject<string>();
        cost.GetComponent<TextMesh>().text = info["cost"].ToObject<int>().ToString();
        desc.GetComponent<TextMesh>().text = info["desc"].ToObject<string>();
        //flavor.GetComponent<TextMesh>().text = info["flavor"].ToObject<string>();

        /*int aux = 0;
        while (GameData.Images[aux].card != info.name && aux < GameData.Images.Count - 1) aux++;
        if (GameData.Images[aux - 1].imagem != null)
            image.GetComponent<SpriteRenderer>().sprite = GameData.Images[aux - 1].imagem;
        else image.GetComponent<SpriteRenderer>().sprite = GameData.Images[GameData.Images.Count - 1].imagem;
        */

        int i, j, cont = 0;
        for (i = 0; i < this.GetComponent<Card>().aspects.Length; i++) {
            for (j = 1; j <= this.GetComponent<Card>().aspects[i]; j++) {
                GameObject newAspect = new GameObject("aspect" + cont.ToString());
                newAspect.transform.SetParent(aspects.transform);
                newAspect.AddComponent<SpriteRenderer>();
                newAspect.transform.position = aspects.transform.position - new Vector3(0, cont * spacament, 0);
                newAspect.transform.localScale = new Vector3(1.6f, 1.6f, 1.0f);
                switch (i) {
                    case 0:
                        newAspect.GetComponent<SpriteRenderer>().sprite = fire;
                        break;
                    case 1:
                        newAspect.GetComponent<SpriteRenderer>().sprite = water;
                        break;
                    case 2:
                        newAspect.GetComponent<SpriteRenderer>().sprite = earth;
                        break;
                    case 3:
                        newAspect.GetComponent<SpriteRenderer>().sprite = air;
                        break;
                }
                newAspect.GetComponent<SpriteRenderer>().sortingOrder = 0;
                cont++;
            }
        }
    }
}
