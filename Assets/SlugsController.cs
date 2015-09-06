﻿using UnityEngine;
using System.Collections;

public class SlugsController : MonoBehaviour
{

    private Rigidbody2D rigid;
    private float speed = 0.15f;
    private int count = 0;
    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        count++;
        if (count < 100)
        {
            transform.localScale = new Vector3(1, 1, 1);
            speed = -0.15f;
        }
        else if (count >= 100 && count < 200)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            speed = 0.15f;
        }
        else
        {
            count = 0;
        }
        rigid.velocity = new Vector2(speed, rigid.velocity.y);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.Instance.alive = false;
        }
    }


}
