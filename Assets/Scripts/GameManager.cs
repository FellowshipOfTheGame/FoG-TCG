using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static char currScene= 'm';

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
}
