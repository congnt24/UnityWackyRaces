using UnityEngine;
using System.Collections;
using System;

public class AttackController : MonoBehaviour
{
    public GameObject BangPrefab;
    // Use this for initialization
    void Start()
    {

    }

    public void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemi")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 100));
            collision.collider.isTrigger = true;
            Destroy(gameObject);

        }
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "DeadLine")
        {
            Destroy(gameObject);
        }
    }
    GameObject bang;
    public void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "DeadLine" || collision.gameObject.tag == "Enemi")
        {
            if (BangPrefab != null)
            {
                bang = Instantiate(BangPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                bang.transform.position = gameObject.transform.position;
                Destroy(gameObject);
            }
            else
            {
                //Destroy(collision.gameObject);
            }
        }
    }
    
}
