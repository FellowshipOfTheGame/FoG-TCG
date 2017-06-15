using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtmButton : MonoBehaviour {

    public static bool open = false;
    float[] mouseDist;
    Color buttonColor;

    void Start() {
        mouseDist = new float[2];
        buttonColor = transform.GetComponent<Image>().color;
    }


    // Update is called once per frame
    void Update() {

        //calcular distancia do mouse
        mouseDist[0] = Input.mousePosition.x - transform.position.x;
        mouseDist[1] = Input.mousePosition.y - transform.position.y - 3.5f;

        if (!open) {
            if (Board.hologram == transform.Find("AtmSlot").GetComponent<Slot>().card) {
                Destroy(Board.hologram);
                Board.hologram = null;
                transform.Find("AtmSlot").GetComponent<Slot>().card = null;
            }
            transform.Find("AtmSlot").gameObject.SetActive(false);
            transform.Find("Frame").gameObject.SetActive(false);
            transform.Find("Text").gameObject.SetActive(true);

            if (transform.Find("AtmSlot").GetComponent<Slot>().IsFull)
                transform.Find("Text").GetComponent<Text>().text = transform.Find("AtmSlot").GetChild(0).name;
            else
                transform.Find("Text").GetComponent<Text>().text = "Atmosphere";

            if (Mathf.Abs(mouseDist[0]) <= 22.5f && Mathf.Abs(mouseDist[1]) <= 72.5f && !Slot.choosingPlace) {
                gameObject.GetComponent<Image>().color = new Color(buttonColor.r + 0.1f, buttonColor.g + 0.1f, buttonColor.b + 0.1f);

                if (Input.GetMouseButtonDown(0))
                    open = true;
            } else
                gameObject.GetComponent<Image>().color = buttonColor;

        } else {
            transform.Find("AtmSlot").gameObject.SetActive(true);
            transform.Find("Frame").gameObject.SetActive(true);
            transform.Find("Text").gameObject.SetActive(false);

            if ((Mathf.Abs(mouseDist[0]) <= 50.0f && Mathf.Abs(mouseDist[1]) <= 72.5f) || Slot.choosingPlace) {
                transform.Find("Frame").GetComponent<Image>().color = new Color(buttonColor.r + 0.1f, buttonColor.g + 0.1f, buttonColor.b + 0.1f);

                if ((Mathf.Abs(mouseDist[0]) <= 50.0f && Mathf.Abs(mouseDist[1]) <= 72.5f) && Input.GetMouseButtonDown(0))
                    open = false;
            } else
                open = false;
        }
    }
}
