using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class QuitDialog : MonoBehaviour {
    public static QuitDialog Instance;
    public Button btn_back, btn_y, btn_no;
    Animator anim;
    public string name;
	// Use this for initialization
	void Start ()
    {
        btn_back.onClick.AddListener(() => BackClick());
        btn_y.onClick.AddListener(() => YClick());
        btn_no.onClick.AddListener(() => NClick());
        anim = GetComponent<Animator>();
        Instance = this;

    }

    private void NClick()
    {
        anim.Play("DiableDialog");
        if (name.Equals("Quit"))
        {
            BoardController.Instance.isPause = false;
        }
        else if (name.Equals("Gameover"))
        {
            Application.LoadLevel("Menu");
        }
    }

    private void YClick()
    {
        if (name.Equals("Quit"))
        {
            Application.LoadLevel("Menu");
        }
        else if (name.Equals("Gameover"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    private void BackClick()
    {
        showDialog("Quit");
    }

    public void showDialog(string name2)
    {
        BoardController.Instance.isPause = true;
        if (name == name2)
        {
            anim.Play("QuitDialog");
        }
    }
}
