using UnityEngine;
using System.Collections;
using System;

public class CameraMoving : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer sprite;
    Vector3 startCameraPos, endCameraPos;
    public static CameraMoving Instance;
    public float sizeOfCamera;
    public float rightPost;
    public GameObject slug, plan1, plan2, bat, flower, rabit;
    public Transform[] slugsPost;
    public Transform[] plan1Post, plan2Post, batPost, flowerPost, rabitPost;
    public ArrayList plan1List = new ArrayList();
    public ArrayList plan1ListGO = new ArrayList();
    public ArrayList plan2List = new ArrayList();
    public ArrayList plan2ListGO = new ArrayList();
    public ArrayList batList = new ArrayList();
    public ArrayList batListGO = new ArrayList();
    public ArrayList flowerList = new ArrayList();
    public ArrayList flowerListGO = new ArrayList();
    public ArrayList rabitList = new ArrayList();
    public ArrayList rabitListGO = new ArrayList();

    // Use this for initialization

    void Start()
    {
        Instance = this;
        //transform.position = Vector3.left * (sprite.bounds.size.x / 2 - gameObject.GetComponent<Camera>().orthographicSize * (float)Screen.width / Screen.height);
        startCameraPos = new Vector3(sprite.bounds.size.x * -1 / 2 + sizeOfCamera, transform.position.y, -10f);
        endCameraPos = new Vector3(sprite.bounds.size.x / 2 - sizeOfCamera, transform.position.y, -10f);
        transform.position = startCameraPos;

        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            child.position = new Vector3(transform.position.x + sizeOfCamera, 0, -10);
        }


    }

    public void Awake()
    {
        sizeOfCamera = gameObject.GetComponent<Camera>().orthographicSize * (float)Screen.width / Screen.height;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.transform.position.x > startCameraPos.x && player.transform.position.x < endCameraPos.x)
        {
            transform.position = new Vector3(player.transform.position.x, 0, -10);
        }
        else if (player.transform.position.x <= startCameraPos.x)
        {
            transform.position = startCameraPos;
        }
        else
        {
            transform.position = endCameraPos;
        }
        rightPost = transform.position.x + sizeOfCamera;

        //Debug.Log("Right: " + rightPost);
        //for (int i = 0; i < slugsPost.Length; i++)
        //{
        //    //Debug.Log("SLUG: " + slugsPost[i].position.x);
        //    if (rightPost > slugsPost[i].position.x - 0.02f && rightPost < slugsPost[i].position.x + 0.02f)
        //    {
        //        StartCoroutine(InitMonter(slugList, slugListGO, slug, slugsPost, i));
        //        break;
        //    }
        //}
        InitMonster(plan1Post, plan1List, plan1ListGO, plan1);
        InitMonster(plan2Post, plan2List, plan2ListGO, plan2);
        InitMonster(batPost, batList, batListGO, bat);
        InitMonster(flowerPost, flowerList, flowerListGO, flower);
        InitMonster(rabitPost, rabitList, rabitListGO, rabit);
    }
    public bool isInitMonter = false;
    private void InitMonster(Transform[] posts, ArrayList list, ArrayList listGo, GameObject prefab)
    {
        for (int i = 0; i < posts.Length; i++)
        {
            if (rightPost > posts[i].position.x+0.1f && rightPost < posts[i].position.x + 0.2f)
            {
                if (PlayerController.Instance.transform.localScale.x ==1 && PlayerController.Instance.moveH>0.05f)
                {
                    StartCoroutine(InitMonter(list, listGo, prefab, posts, i));
                    break;
                }
            }
        }
    }
    private IEnumerator InitMonter(ArrayList list, ArrayList listGO, GameObject prefab, Transform[] trans, int i)
    {
        if (!list.Contains(i))
        {
            list.Add(i);
            //GameObject g = Instantiate(prefab, trans[i].position, Quaternion.identity) as GameObject;
            //listGO.Add(g);
        }
        yield return new WaitForFixedUpdate();
    }
    
    //Handle Generate using Collider
    public void OnTriggerEnter2D(Collider2D collision)
    {

    }




}
