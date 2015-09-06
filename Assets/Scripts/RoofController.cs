using UnityEngine;
using System.Collections;

public class RoofController : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 point = collision.contacts[1].normal;
            if (point.ToString() == Vector2.up.ToString())
            {
                //Debug.Log("Collision up");
                //collision.collider.isTrigger = true;
            }
        }

    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        collider.isTrigger = true;
    }
    public void OnTriggerExit2D(Collider2D collider)
    {
        collider.isTrigger = false;
    }


}
