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




    // Use this for initialization
    void Start () {
        name = original.name + "_proj";
        title.GetComponent<TextMesh>().text = original.GetComponent<AddCardInformation>().title.GetComponent<TextMesh>().text;
        cost.GetComponent<TextMesh>().text = original.GetComponent<AddCardInformation>().cost.GetComponent<TextMesh>().text;
        desc.GetComponent<TextMesh>().text = original.GetComponent<AddCardInformation>().desc.GetComponent<TextMesh>().text;
        flavor.GetComponent<TextMesh>().text = original.GetComponent<AddCardInformation>().flavor.GetComponent<TextMesh>().text;
        float spacament = original.GetComponent<AddCardInformation>().spacament * (this.transform.localScale.x / original.transform.localScale.x);

        int i;
        for (i = 0; i < original.GetComponent<AddCardInformation>().aspects.transform.childCount; i++) {
            GameObject newAspect = Instantiate(original.GetComponent<AddCardInformation>().aspects.transform.GetChild(i).gameObject, aspects.transform);
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
