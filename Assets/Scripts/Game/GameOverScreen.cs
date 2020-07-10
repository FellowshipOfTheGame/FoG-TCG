using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

    public Sprite draw, victory1, victory2;

    public Animator anim;
    Board board;
    public Image scr;

    public void Show() {
        Camera.main.GetComponent<Raycaster>().enabled = false;
        board = FindObjectOfType<Board>() as Board;
        if (Board.winner == 1) {
            scr.sprite = victory1;
        } else if (Board.winner == 2) {
            scr.sprite = victory2;
        } else {
            scr.sprite = draw;
        }
        anim.SetTrigger("show");
    }

    public void Rematch() {
        anim.SetTrigger("reset");
        board.ResetGame();
    }

    public void Quit() {
        SceneManager.LoadScene("Menu");
    }

}