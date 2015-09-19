using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class PlayerController : MonoBehaviour
{
    private float speed = 1f;
    private Rigidbody2D mrigidbody;
    public bool touchGround = true; // Check player on the ground
    public bool alive = true;   //Checking if the player is alive or dead
    private Animator animator;  //Handle animation of player
    public static PlayerController Instance;    //Just for Singleton Pattern
    public float moveH; //Moving of player [(0, 1]
    private float nextJump; //Duration time of 2 jump

    public int gemCount, lifeCount, boneCount;//Counting the number of gem, bone, life are collected by Player
    public int skillNum;    //Wich skill will be used by Player
    public int hearthCount;//Number of heart
    private int jumpCount = 0;
    public bool canFly;


    private float nextBombTime, durationBomb=0.5f;

    private float nextDieTime, durationDie = 1f;
    public  GameObject bitePrefab, bombPrefab, bulletPrefab;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        mrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Debug.Log("Screen: "+(float)Screen.width /Screen.height);
        //Initialize count number
        gemCount = 0;
        lifeCount = 0;
        boneCount = 0;
        hearthCount = 3;
        skillNum = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (canFly)
        {
            if (!touchGround)
            {
            
                if (mrigidbody.velocity.y < 0)
                {
                    if (mrigidbody.velocity.y < -0.05f)
                    {
                        if (jumpCount == 1)
                        {
                            animator.Play("Fly");
                            mrigidbody.velocity *= 0.75f;
                        }
                    }
                    else
                    {
                        //jumpCount = 0;
                    }
                }
                else
                {
                    jumpCount = 0;
                    animator.Play("Idle");
                }
            }
            else
            {
                if (jumpCount==1)
                {
                    animator.Play("Idle");
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!BoardController.Instance.isPause)
        {
            //Movement
            PayerWalk();
            if (!alive) //Die
            {
                StartCoroutine(PlayerDie());
                
            }
            //Jumping
            if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButton("Jump"))
            {
                PlayerJump();
            }
            //Atttack
            if (Input.GetKeyDown(KeyCode.A) || CrossPlatformInputManager.GetButton("Attack"))
            {
                PlayerAttack();
            }
            //Using skill
            if (Input.GetKeyDown(KeyCode.L) || CrossPlatformInputManager.GetButton("Select"))
            {
                USingSkill();
            }
        }
    }

    private IEnumerator PlayerDie()
    {
        BoardController.Instance.isPause = true;
        mrigidbody.velocity = Vector3.zero;
        animator.Play("Die");
        yield return new WaitForSeconds(0.5f);
        PlayerJump();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        if (lifeCount > 0)
        {
            StartCoroutine(restart());
        }
        else//lifeCount ==0
        {
            //GameOver
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        Application.LoadLevel("Menu");
        
    }

    private void USingSkill()
    {
		BoardController.Instance.PickSkillFunc (boneCount);
        boneCount = 0;
    }


    //Handle attacking of player
    private void PlayerAttack()
    {
        if (alive)
        {
            
            GameObject clone=null;
            if (skillNum==0 || skillNum ==4)//Bite
            {
                if (Time.time - nextBombTime >= durationBomb)
                {
                    nextBombTime = Time.time;
                    //UnityEngine.Object bitrPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bite.prefab", typeof(GameObject));
                    clone = Instantiate(bitePrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    clone.transform.position = transform.position + new Vector3(0.06f * gameObject.transform.localScale.x, 0.02f, 0.0f);

                    //animator.SetBool("isAttack", true);
                    animator.Play("Bite");
                    StartCoroutine(sleepAttack(clone));
                }
            }
            else if (skillNum == 1)//Through Bomb
            {
                if (Time.time - nextBombTime >= durationBomb)
                {
                    nextBombTime = Time.time;
                    //UnityEngine.Object bitrPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bomb.prefab", typeof(GameObject));
                    clone = Instantiate(bombPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    clone.transform.position = transform.position + new Vector3(0.0f, 0.25f, 0.0f);


                    //animator.SetBool("isAttack", true);
                    animator.Play("Bite");
                    StartCoroutine(sleepAttack(clone));
                }
            }
            else if (skillNum == 2)//Through Bullet
            {
                if (Time.time - nextBombTime >= durationBomb)
                {
                    nextBombTime = Time.time;
                    //UnityEngine.Object bitrPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bullet.prefab", typeof(GameObject));
                    clone = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    clone.transform.position = transform.position + new Vector3(0.0f, 0.0f, 0.0f);


                    //animator.SetBool("isAttack", true);
                    animator.Play("Bite");
                    StartCoroutine(sleepAttack(clone));
                }
            }
        }

    }

    //Duration between 2 time attack
    private IEnumerator sleepAttack(GameObject clone)
    {
        switch (skillNum)
        {
            case 1:

                yield return new WaitForSeconds(0.1f);
                clone.AddComponent<Rigidbody2D>();
                clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(120 * gameObject.transform.localScale.x, 30));
                yield return new WaitForSeconds(0.2f);
                break;
            case 2:
                yield return new WaitForSeconds(0.1f);
                //clone.AddComponent<Rigidbody2D>();
                //clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 120 * gameObject.transform.localScale.x));
                yield return new WaitForSeconds(0.4f);
                break;
            default:
                yield return new WaitForSeconds(0.2f);
                Destroy(clone);
                break;
        }
        animator.SetBool("isAttack", false);
        //
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
        //When touch Enemi, 
        if (coll.gameObject.tag == "Enemi")
        {
            if (Time.time - nextDieTime >= durationDie)
            {
                nextDieTime = Time.time;
                gameObject.layer = LayerMask.NameToLayer("Player");
                coll.gameObject.layer = LayerMask.NameToLayer("Enemi");
                animator.Play("LoseHeart");
                StartCoroutine(Sleep1Second(coll.gameObject));
                if (--hearthCount == 0)
                {
                    Dead();
                }
            }
        }
    }

    public void Dead()
    {
        touchGround = true;
        alive = false;
    }

    private IEnumerator Sleep1Second(GameObject coll)
    {
        yield return new WaitForSeconds(1f);
        gameObject.layer = LayerMask.NameToLayer("Player");
        coll.gameObject.layer = LayerMask.NameToLayer("Default");

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
        if (other.gameObject.tag == "Bone")
        {
            boneCount++;
            
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "BulletEnemi")
        {
            
                animator.Play("LoseHeart");
                if (--hearthCount == 0)
            {
                animator.Play("Die");
                Dead();
                }
            Destroy(other.gameObject);
        }
    }


    //Handle walking of player
    public void PayerWalk()
    {
		
		moveH = CrossPlatformInputManager.GetAxis ("Horizontal");
        /* if (SystemInfo.deviceType == DeviceType.Desktop)
        {
			moveH = Input.GetAxis ("Horizontal");
       	}
       	else
         {
       //Debug.Log("move H: "+moveH);
			//moveH = MoveBack.Instance.getDirection() + MoveForward.Instance.getDirection();

         }
*/

        // Vector2 direction = MovingGamePad.Instance.getDirection();
        if (moveH != 0.0f)
        {
            if (moveH > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (moveH < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
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
        if (mrigidbody.velocity.y < -0.01)
        {
            jumpCount = 1;
        }
            if (Time.time > nextJump)
        {
            nextJump = Time.time + 0.2f;
        if (touchGround)
            {
                touchGround = false;
                mrigidbody.velocity = mrigidbody.velocity * 0.5f;
                mrigidbody.AddForce(Vector2.up * 140);
            }

        }
        else
        {
            
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
        yield return new WaitForSeconds(2f);
        lifeCount--;
        Application.LoadLevel(Application.loadedLevel);
    }


}
