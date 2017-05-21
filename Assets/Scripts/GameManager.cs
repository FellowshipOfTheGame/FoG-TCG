using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static char currScene= 'g';

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
}
