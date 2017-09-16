using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionScript : MonoBehaviour {

    public GameObject original;
    public GameObject title;
    public GameObject image;
    public GameObject desc;
    public GameObject flavor;
    public GameObject cost;
    public GameObject atk;
    public GameObject hp;
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
        title.GetComponent<TextMesh>().text = original.GetComponent<AddCardInformationSemCanvas>().title.GetComponent<TextMesh>().text;
        cost.GetComponent<TextMesh>().text = original.GetComponent<AddCardInformationSemCanvas>().cost.GetComponent<TextMesh>().text;
        desc.GetComponent<TextMesh>().text = original.GetComponent<AddCardInformationSemCanvas>().desc.GetComponent<TextMesh>().text;
        flavor.GetComponent<TextMesh>().text = original.GetComponent<AddCardInformationSemCanvas>().flavor.GetComponent<TextMesh>().text;

        if(original.GetComponent<Card>().type == 'c') {
            atk.GetComponent<TextMesh>().text = original.GetComponent<AddCardInformationSemCanvas>().atk.GetComponent<TextMesh>().text;
            hp.GetComponent<TextMesh>().text = original.GetComponent<AddCardInformationSemCanvas>().hp.GetComponent<TextMesh>().text;
        }

        int i;
        for (i = 0; i < original.GetComponent<AddCardInformationSemCanvas>().aspects.transform.childCount; i++) {
            GameObject newAspect = Instantiate(original.GetComponent<AddCardInformationSemCanvas>().aspects.transform.GetChild(i).gameObject, aspects.transform);
            newAspect.transform.position = aspects.transform.position - new Vector3(0, i * spacament, 0);
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
