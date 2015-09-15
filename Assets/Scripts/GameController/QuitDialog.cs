using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class QuitDialog : MonoBehaviour {
    public Button btn_back, btn_y, btn_no;
    Animator anim;
	// Use this for initialization
	void Start ()
    {
        btn_back.onClick.AddListener(() => BackClick());
        btn_y.onClick.AddListener(() => YClick());
        btn_no.onClick.AddListener(() => NClick());
        anim = GetComponent<Animator>();
    }

    private void NClick()
    {

        anim.Play("DiableDialog");
        BoardController.Instance.isPause = false;
    }

    private void YClick()
    {
        Application.LoadLevel("Menu");
    }

    private void BackClick()
    {
        anim.Play("QuitDialog");
        BoardController.Instance.isPause = true;
    }

    // Update is called once per frame
    void Update () {
    }
}
