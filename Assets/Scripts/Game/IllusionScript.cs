using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionScript : MonoBehaviour {

    public GameObject original;
    public TextMesh title;
    public SpriteRenderer image;
    public TextMesh desc;
    public TextMesh flavor;
    public TextMesh cost;
    public TextMesh atk;
    public TextMesh hp;

    public TextMesh modifier;
    public SpriteRenderer effect;
    public GameObject aspects;
    [Space(5)]
    public float spinVelocity;
    public float maxAngle;
    float angle;
    [Space(5)]
    public float spacament;




    // Use this for initialization
    void Start () {
        name = original.name + "_proj";
        title.text = original.GetComponent<AddCardInformationSemCanvas>().title.text;
        cost.text = original.GetComponent<AddCardInformationSemCanvas>().cost.text;
        desc.text = original.GetComponent<AddCardInformationSemCanvas>().desc.text;
        flavor.text = original.GetComponent<AddCardInformationSemCanvas>().flavor.text;
        image.sprite = original.GetComponent<AddCardInformationSemCanvas>().image.sprite;

        Card info = original.GetComponent<Card>();
        if (info.haveModifier){
            modifier.text = info.am.name;
            effect.sprite = info.am.modifier;
            modifier.gameObject.SetActive(true);
        }else{
            modifier.gameObject.SetActive(false);
        }

        float r = 3.0f / image.sprite.bounds.extents.x;
        image.transform.localScale = Vector3.one * r;

        if (original.GetComponent<Card>().type == 'c') {
            atk.transform.parent.gameObject.SetActive(true);
            atk.text = original.GetComponent<AddCardInformationSemCanvas>().atk.text;
            hp.text = original.GetComponent<AddCardInformationSemCanvas>().hp.text;
        }

        int i;
        for (i = 0; i < original.GetComponent<AddCardInformationSemCanvas>().aspects.transform.childCount; i++) {
            GameObject newAspect = Instantiate(original.GetComponent<AddCardInformationSemCanvas>().aspects.transform.GetChild(i).gameObject, aspects.transform);
            newAspect.transform.position = aspects.transform.position + new Vector3(i * spacament, 0.0f, 0.0f);
            if (i > 2)
                newAspect.transform.position += new Vector3(0.45f, 0.0f, 0.0f);
        }

        angle = 0;
    }
	
	// Update is called once per frame
	void Update () {

        this.transform.Rotate(Vector3.up, -angle);
        angle += spinVelocity;
        this.transform.Rotate(Vector3.up, angle);

        if (angle < -maxAngle || angle > maxAngle)
            spinVelocity = -spinVelocity;

    }
}
