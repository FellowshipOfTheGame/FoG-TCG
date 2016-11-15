using UnityEngine;
using System.Collections;

public class DeckListManager : MonoBehaviour {

    Transform currentChild;
    Transform nextChild;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        /*if (transform.childCount > 0)
        {
            currentChild = transform.GetChild(0);
            for (int i = 1; i < transform.childCount; i++)
            {
                nextChild = transform.GetChild(i);
                if (string.Compare(currentChild.name, nextChild.name) > 0)
                {
                    currentChild.SetSiblingIndex(i);
                }

                currentChild = nextChild;

            }
        }*/

	}
}
