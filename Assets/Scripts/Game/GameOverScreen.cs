using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

    public Sprite victory;
    public Sprite draw;
    public Sprite defeat;
    Board board;
    GameObject scr1, scr2;

    public void Show() {
        Camera.main.GetComponent<Raycaster>().enabled = false;
        board = FindObjectOfType<Board>() as Board;
        scr1 = this.transform.Find("Screen1").gameObject;
        scr2 = this.transform.Find("Screen2").gameObject;
        if (Board.winner == 1) {
            scr1.transform.Find("Image").GetComponent<Image>().sprite = victory;
            scr2.transform.Find("Image").GetComponent<Image>().sprite = defeat;
        } else if (Board.winner == 2) {
            scr1.transform.Find("Image").GetComponent<Image>().sprite = defeat;
            scr2.transform.Find("Image").GetComponent<Image>().sprite = victory;
        } else {
            scr1.transform.Find("Image").GetComponent<Image>().sprite = draw;
            scr2.transform.Find("Image").GetComponent<Image>().sprite = draw;
        }
    }

    public void Rematch(int index) {
        Camera.main.GetComponent<Raycaster>().enabled = true;
        board.ResetGame();
    }

    public void Quit() {
        Camera.main.GetComponent<Raycaster>().enabled = true;
        SceneManager.LoadScene("Menu");
    }

}