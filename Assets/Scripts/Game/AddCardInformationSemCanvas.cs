using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddCardInformationSemCanvas : MonoBehaviour {

    public Card info;
    public TextMesh title;
    public SpriteRenderer image;
    public TextMesh desc;
    public TextMesh flavor;
    public TextMesh cost;
    public TextMesh atk;
    public TextMesh hp;
    public GameObject aspects;
    [Space(5)]
    public SpriteRenderer minImage;
    public TextMesh minAtk;
    public TextMesh minHp;

    [Space(10)]
    public Sprite fire;
    public Sprite water;
    public Sprite earth;
    public Sprite air;
    public float spacament;
    
    [Space(10)]
    public SpriteRenderer frame;
    public Color terrain, atm, spell;

    // Use this for initialization
    public void Initialize() {
        //add information in Card class
        this.GetComponent<Card>().cost = info["cost"].ToObject<int>();
        this.GetComponent<Card>().type = info["type"].ToObject<char>();
        this.GetComponent<Card>().aspects = info["aspects"].ToObject<int[]>();
        if(info["type"].ToObject<char>() == 'c') {
            atk.transform.parent.gameObject.SetActive(true);
            this.GetComponent<Card>().atk = info["atk"].ToObject<int>();
            atk.text = info["atk"].ToObject<int>().ToString();
            this.GetComponent<Card>().hp = info["hp"].ToObject<int>();
            hp.text = info["hp"].ToObject<int>().ToString();
            minAtk.transform.parent.parent.gameObject.SetActive(true);
            minAtk.text = atk.text;
            minHp.text = hp.text;
        }else{
            //if(info["type"].ToObject<char>() == 't') frame.color = terrain;
            //if(info["type"].ToObject<char>() == 'a') frame.color = atm;
            //if(info["type"].ToObject<char>() == 's') frame.color = spell;
        }
        
        //draw infomation
        name = info["title"].ToObject<string>();
        title.text = info["title"].ToObject<string>();
        cost.text = info["cost"].ToObject<int>().ToString();
        string d = info["desc"].ToObject<string>();
        desc.text = "";

        int lineSize = 27;
        while (d.Length > lineSize) {
            if (d.Substring(lineSize, 1) != " ") {
                string a = d.Substring(0, lineSize);
                desc.text += a.Substring(0, a.LastIndexOf(" ")) + "\n";
                d = d.Substring(a.LastIndexOf(" "));
            }else {
                desc.text += d.Substring(0, lineSize) + "\n";
                d = d.Substring(lineSize);
            }
        }
        desc.text += d;

        //flavor.GetComponent<TextMesh>().text = info["flavor"].ToObject<string>();

        Sprite aux = Resources.Load<Sprite>(name.ToLower());
        if (aux != null) { image.sprite = aux; }
        /*
        if (GameManager.chosenDeck != null) {
            int aux = 0;
            while (GameData.Images[aux++].card != info.name && aux < GameData.Images.Count - 1) ;
            if (GameData.Images[aux - 1].imagem != null)
                image.sprite = GameData.Images[aux - 1].imagem;
            else
                image.sprite = GameData.Images[GameData.Images.Count - 1].imagem;
        }*/
        float r = 3.0f / image.sprite.bounds.extents.x;
        image.transform.localScale = Vector3.one * r;
        minImage.sprite = image.GetComponent<SpriteRenderer>().sprite;
        r = 3.7f / minImage.sprite.bounds.extents.y;
        minImage.transform.localScale = Vector3.one * r;

        int i, j, cont = 0;
        for (i = 0; i < this.GetComponent<Card>().aspects.Length; i++) {
            for (j = 1; j <= this.GetComponent<Card>().aspects[i]; j++) {
                GameObject newAspect = new GameObject("aspect" + cont.ToString());
                newAspect.transform.SetParent(aspects.transform);
                newAspect.AddComponent<SpriteRenderer>();
                newAspect.transform.position = aspects.transform.position + new Vector3(cont * spacament, 0, 0);
                if (cont > 2)
                    newAspect.transform.position += new Vector3(0.135f, 0.0f, 0.0f);
                newAspect.transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
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
