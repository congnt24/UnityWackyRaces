using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private float speed = 2f;
    private Rigidbody2D mrigidbody;
    private bool touchGround = true;
    private bool alive = true;
    private Animator animator;
    // Use this for initialization
    void Start()
    {
        mrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

            //Movement
            float moveH = Input.GetAxis("Horizontal");
            if (moveH != 0.0f)
            {
                animator.SetBool("isWalk", true);
            }
            else
            {
                animator.SetBool("isWalk", false);
            }
            mrigidbody.velocity = new Vector3(moveH * speed, mrigidbody.velocity.y, 0.0f);
        //Jumping
        if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log("J");
                //if (touchGround)
                //{
                    PlayerJump();
               // }
            }
        

    }


    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag == "Ground")
        {
            touchGround = true;
            /*Vector2 pointOfContact = coll.contacts[0].normal;//Grab the normal of the contact point we touched
                                                             //Detect which side of the collider we touched
            if (pointOfContact == new Vector2(-1, 0))
            {
                Debug.Log("We touched the left side of the enemy!");
                alive = false;
            }

            if (pointOfContact == new Vector2(1, 0))
            {
                Debug.Log("We touched the right side of the enemy!");
                alive = false;
            }

            if (pointOfContact == new Vector2(0, -1))
            {
                Debug.Log("We touched the enemy's bottom!");
            }

            if (pointOfContact == new Vector2(0, 1))
            {
                Debug.Log("We touched the top of the enemy!");
            }*/
        }
        if (coll.gameObject.tag == "Enemi")
        {
            //			Destroy(gameObject);
            alive = false;
            PlayerJump();
            gameObject.GetComponentInChildren<BoxCollider2D>().isTrigger = true;
        }
    }

    public void PlayerJump()
    {
        mrigidbody.AddForce(Vector2.up * 200, ForceMode2D.Force);
        touchGround = false;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            Destroy(other.gameObject);
        }
    }

}
