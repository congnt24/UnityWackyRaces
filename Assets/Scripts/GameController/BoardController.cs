﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BoardController : MonoBehaviour
{

    public Text gemCount, lifesCount, timeCount;
    public Image[] bones;
    public Image[] hearts;
    public Image[] slotSkill;
    private float time;
    public static BoardController Instance;
    public Button pickSkill, btn_back;
    public bool isPause = false;
    int leftTime;
    // Use this for initialization
    void Start()
    {
        time = Time.time;
        Instance = this;
        pickSkill.onClick.AddListener(()=> PickSkillFunc(PlayerController.Instance.boneCount));
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!isPause)
        {
            gemCount.text = string.Format("X {0:00}", PlayerController.Instance.gemCount);
            leftTime = (int)(180 - (Time.time - time));
            if (leftTime>0)
            {
                timeCount.text = "" + leftTime;
            }
            else
            {
                timeCount.text = "0";
                PlayerController.Instance.Dead();
            }
            lifesCount.text = string.Format("X {0:00}", PlayerController.Instance.lifeCount);
            showBone(PlayerController.Instance.boneCount);
            Showheart();
            if (Input.GetKeyDown(KeyCode.S))
            {
                PickSkillFunc(PlayerController.Instance.boneCount);
            }

        }
    }

    public void PickSkillFunc(int j)
    {
        if (j==0)
        {
            return;
        }
        PlayerController.Instance.boneCount = 0;
        if (j == 4)
        {
            if (PlayerController.Instance.hearthCount < 4)
            {
                PlayerController.Instance.hearthCount++;
            }
            return;
        }
        if (j == 3)
        {
            PlayerController.Instance.canFly = true;
            slotSkill[2].gameObject.SetActive(true);
            Debug.Log("You can fly");
        }
        if (j==2 || j==1)
        {
            showSlot(j-1);
            PlayerController.Instance.skillNum = j;
        }
    }

    public void showSlot(int j)
    {
        for (int i = 0; i < 2; i++)
        {
            if (i==j)
            {
                slotSkill[i].gameObject.SetActive(true);
            }
            else
            {
                slotSkill[i].gameObject.SetActive(false);
            }
        }
    }

    private void showBone(int j)
    {
        if (j==5)
        {
            j = 1;
            PlayerController.Instance.boneCount = 1;
        }
        for (int i = 0; i < bones.Length; i++)
        {
            if (i==j-1)
            {
                bones[i].gameObject.SetActive(true);

            }
            else
            {
                bones[i].gameObject.SetActive(false);
            }
        }
    }
    private void Showheart()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i< PlayerController.Instance.hearthCount)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }
}
