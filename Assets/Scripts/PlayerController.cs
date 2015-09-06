using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    private float speed = 1f;
    private Rigidbody2D mrigidbody;
    private bool touchGround = true;
    public bool alive = true;
    private Animator animator;
    public static PlayerController Instance;
    public float moveH;
    private float nextJump;

    public int gemCount, lifeCount;
    // Use this for initialization
    void Start()
    {
        Instance = this;
        mrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Debug.Log("Screen: "+(float)Screen.width /Screen.height);
        gemCount = 0;
        lifeCount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        PayerWalk();
        if (!alive) //Die
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            PlayerJump();
            //StartCoroutine(restart());
        }

    }

    void FixedUpdate()
    {

        //Jumping
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Space) || JumpingController.Instance.getBoolJump())
        {
            PlayerJump();
        }

    }


    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag == "Ground")
        {
            if (getSide(coll) == 2 || getSide(coll) == 4)
            {
                //Ma sat = 0
                //touchGround = false;
            }
            else if (getSide(coll) == 1) // Neu o ben trn ground thì dc phep jump
            {
                //gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                touchGround = true;
            }

        }

        if (coll.gameObject.tag == "Roof" && getSide(coll) == 1)
        {
            touchGround = true;
        }
        //When touch Enemi
        if (coll.gameObject.tag == "Enemi")
        {
            alive = false;
            
        }
    }

    //Keep jumpable when move on the ground
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //touchGround = true;
        }
    }
    // Deny jumping when exit the ground
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //touchGround = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Dinamon")
        {
            gemCount++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Life")
        {
            lifeCount++;
            Destroy(other.gameObject);
        }
    }


    //Handle walking of player
    public void PayerWalk()
    {

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {

            moveH = Input.GetAxis("Horizontal");
        }
        else
        {
            //Debug.Log("move H: "+moveH);
            moveH = MoveBack.Instance.getDirection() + MoveForward.Instance.getDirection();

        }
        

       // Vector2 direction = MovingGamePad.Instance.getDirection();
        if (moveH != 0.0f)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }

        mrigidbody.velocity = new Vector3(moveH * speed, mrigidbody.velocity.y, 0.0f);
    }

    //Handle jumpping of player
    public void PlayerJump()
    {
        if (Time.time > nextJump)
        {
            nextJump = Time.time + 0.2f;
        if (touchGround)
            {
                touchGround = false;
                mrigidbody.AddForce(Vector2.up * 170);
            }

        }
    }

    //Get side of colision
    public int getSide(Collision2D coll)
    {
        Vector2 pointOfContact = coll.contacts[0].normal;//Grab the normal of the contact point we touched
        //Detect which side of the collider we touched
        if (pointOfContact.ToString() == new Vector2(-1, 0).ToString())
        {
            return 4; //left
        }

        if (pointOfContact.ToString() == new Vector2(1, 0).ToString())
        {
            return 2;
        }

        if (pointOfContact.ToString() == new Vector2(0, -1).ToString())
        {
            return 3;
        }

        if (pointOfContact.ToString() == new Vector2(0, 1).ToString())
        {
            return 1; //top
        }
        return 0;
    }

    //Restart
    IEnumerator restart()
    {
        yield return new WaitForSeconds(1f);
        Application.LoadLevel(Application.loadedLevel);
    }


}
