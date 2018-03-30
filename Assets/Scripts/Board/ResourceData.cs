using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceData : MonoBehaviour {
    public GameObject[] allTerrains;


	// Use this for initialization
	void Start () {
		allTerrains = Resources.LoadAll<GameObject>("Terrain/");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
