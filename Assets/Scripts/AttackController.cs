using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemi")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 100));
            collision.collider.isTrigger = true;
        }
    }
}
