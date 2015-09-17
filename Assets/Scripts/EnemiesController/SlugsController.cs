using UnityEngine;
using System.Collections;
using System;

public class SlugsController : MonoBehaviour
{

    private Rigidbody2D rigid;
    private float speed = 0.2f;
    private int count = 0;
    public int range;
    public string name;
    public bool touchGround = false;
    private float batPost;
    public GameObject FlowerBullet;
    private bool flowerShoot=false;


    public GameObject orgin;
    // Use this for initialization
    void Start()
    {
        
        rigid = GetComponent<Rigidbody2D>();
        if (name.Equals("Bat"))
        {
            batPost = transform.position.x - 3.5f;
            Debug.Log(transform.position + "-" + batPost);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!BoardController.Instance.isPause)
        {
            if (!flowerShoot)
            {


                //Rabit
                if(name.Equals("Rabit"))
                {
                    count++;
                    if (count < range)
                    {
                        //transform.localScale = new Vector3(-1, 1, 1);
                        speed = -0.2f;
                    }
                    else if (count >= range && count <= range * 2)
                    {
                        //transform.localScale = new Vector3(1, 1, 1);
                        speed = 0.2f;
                    }
                    else
                    {
                        count = 0;
                    }
                    transform.position += Vector3.right * speed * 2 * Time.deltaTime;

                }

                //Slug
                if (name.Equals("Slug"))
                {

                    //Debug.Log("Slug: "+transform.position);
                    count++;
                    if (count < range)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                        speed = -0.2f;
                    }
                    else if (count >= range && count < range * 2)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                        speed = 0.2f;
                    }
                    else
                    {
                        count = 0;
                    }
                    rigid.velocity = new Vector2(speed, rigid.velocity.y);

                }
                else if (name.Equals("Plan"))
                {
                    if (touchGround)
                    {
                        StartCoroutine(PlanJump());
                        touchGround = false;
                    }
                }
                else if (name.Equals("Plan2"))
                {
                    if (touchGround)
                    {
                        StartCoroutine(PlanJump2());
                        touchGround = false;
                    }
                }
                else if (name.Equals("Bat"))
                {

                    if (transform.position.x >= (CameraMoving.Instance.transform.position.x - CameraMoving.Instance.sizeOfCamera))
                    {
                        transform.position += Vector3.left * speed * 2 * Time.deltaTime;
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
                else if (name.Equals("Flower"))
                {

                    if (transform.position.x >= (CameraMoving.Instance.transform.position.x - CameraMoving.Instance.sizeOfCamera))
                    {
                        StartCoroutine(FlowerShoot());
                    }
                }
            }
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
    }

    private IEnumerator FlowerShoot()
    {
        Instantiate(FlowerBullet, transform.position, Quaternion.identity);
        flowerShoot = true;
        yield return new WaitForSeconds(10);
        flowerShoot = false;
    }

    public int jumpcount=0;
    private IEnumerator PlanJump()
    {
        
            if (jumpcount==0)
            {
            yield return new WaitForSeconds(1.5f);
            rigid.AddForce(Vector2.up * 130);
                //yield return new WaitForSeconds(10f);
                jumpcount = 1;
            }
            else
            {
                jumpcount = 0;
                rigid.AddForce(Vector2.up * 160);
            }
    }

    private IEnumerator PlanJump2()
    {
        if (jumpcount == 0)
        {
            yield return new WaitForSeconds(1.5f);
            rigid.AddForce(Vector2.up * 160);
            //yield return new WaitForSeconds(10f);
            jumpcount = 1;
        }
        else if (jumpcount == 1)
        {
            jumpcount = 2;
            rigid.AddForce(new Vector2(-36f, 140f));
            //rigid.AddForce(new Vector2(0.6f, -1) * (-120));
            //yield return new WaitForSeconds(10f);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            jumpcount = 0;
            rigid.AddForce(new Vector2(36f, 140f));
        }
    }

    

    private IEnumerator sleepNSecond(float n)
    {
        yield return new WaitForSeconds(n);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            touchGround = true;
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {

            if (name.Equals("Slug"))
            {
                //int i = CameraMoving.Instance.slugListGO.IndexOf(gameObject);
                //if (i != -1)
                //{
                //    CameraMoving.Instance.slugListGO.RemoveAt(i);
                //    CameraMoving.Instance.slugList.RemoveAt(i);
                //}
            }
            else
            if (name.Equals("Plan"))
            {
                //orgin.SetActive(true);
                //int i = CameraMoving.Instance.plan1ListGO.IndexOf(gameObject);
                //if (i != -1)
                //{
                //    CameraMoving.Instance.plan1ListGO.RemoveAt(i);
                //    CameraMoving.Instance.plan1List.RemoveAt(i);
                //}
            }
            else
            if (name.Equals("Plan2"))
            {
                //int i = CameraMoving.Instance.plan2ListGO.IndexOf(gameObject);
                //if (i != -1)
                //{
                //    CameraMoving.Instance.plan2ListGO.RemoveAt(i);
                //    CameraMoving.Instance.plan2List.RemoveAt(i);
                //}
            }
            else
            if (name.Equals("Bat"))
            {
                transform.parent.gameObject.AddComponent<Rigidbody2D>();
                //Debug.Log("XXXXXXXXXXXXXXXXX");
                //int i = CameraMoving.Instance.batListGO.IndexOf(gameObject);
                //if (i != -1)
                //{
                //    CameraMoving.Instance.batListGO.RemoveAt(i);
                //    CameraMoving.Instance.batList.RemoveAt(i);
                //}
            }
            else
            if (name.Equals("Flower"))
            {
                //int i = CameraMoving.Instance.flowerListGO.IndexOf(gameObject);
                //if (i != -1)
                //{
                //    CameraMoving.Instance.flowerListGO.RemoveAt(i);
                //    CameraMoving.Instance.flowerList.RemoveAt(i);
                //}
            }
            else
            if (name.Equals("Rabit"))
            {
                transform.parent.gameObject.AddComponent<Rigidbody2D>();
                //int i = CameraMoving.Instance.rabitListGO.IndexOf(gameObject);
                //if (i != -1)
                //{
                //    CameraMoving.Instance.rabitListGO.RemoveAt(i);
                //    CameraMoving.Instance.rabitList.RemoveAt(i);
                //}
            }

            if (name.Equals("Bat") || name.Equals("Rabit"))
            {
                transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100));
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100));
            }


            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            //orgin.SetActive(true);



        }
        if (collision.gameObject.tag == "DeadLine")
        {
            if (name.Equals("Rabit") || name.Equals("Bat"))
            {
                //Destroy(gameObject.transform.parent.gameObject);
                //orgin.SetActive(true);
            }
            Destroy(gameObject);
            orgin.SetActive(true);
        }
    }

    
}
