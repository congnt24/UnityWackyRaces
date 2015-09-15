using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MenuController : MonoBehaviour {

    public Button btn_start, btn_quit;
	// Use this for initialization
	void Start ()
    {
        btn_start.onClick.AddListener(() => StartGame());
        btn_quit.onClick.AddListener(() => QuitGame());
    }

    // Update is called once per frame
    void Update () {

    }

    //On Click
    private void QuitGame()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        Application.LoadLevel("Game");
    }
}
