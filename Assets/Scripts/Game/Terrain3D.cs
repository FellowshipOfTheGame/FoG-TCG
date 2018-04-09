using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain3D : MonoBehaviour {

    public Transform model;
    Vector3 originalPos, destiny;
    public float hidePos, offset;
    bool move = false;
	// Use this for initialization
	void Start () {
        originalPos = model.position + Vector3.forward * offset;
        model.position = originalPos + Vector3.forward * hidePos;
        ascend();
	}
	
	// Update is called once per frame
	void Update () {
		if (move) {
            if (model.position != destiny)
                model.position = Vector3.Lerp(model.position, destiny, 0.3f);
            else
                move = false;
        }
	}

    public void ascend() {
        destiny = originalPos;
        move = true;
    }

    public void descend() {
        destiny = originalPos + Vector3.forward * hidePos;
        move = true;
    }
}
