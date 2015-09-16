using UnityEngine;
using System.Collections;

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
    public ArrayList slugList = new ArrayList();
    public ArrayList slugListGO = new ArrayList();
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
    }

    public void Awake()
    {
        sizeOfCamera = gameObject.GetComponent<Camera>().orthographicSize * (float)Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
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
        for (int i = 0; i < slugsPost.Length; i++)
        {
            //Debug.Log("SLUG: " + slugsPost[i].position.x);
            if (rightPost > slugsPost[i].position.x - 0.1f && rightPost < slugsPost[i].position.x + 0.1f)
            {
                    if (!slugList.Contains(i))
                    {
                        GameObject g = Instantiate(slug, slugsPost[i].position, Quaternion.identity) as GameObject;
                        slugList.Add(i);
                        slugListGO.Add(g);
                 }
                    break;
            }
        }
        for (int i = 0; i < plan1Post.Length; i++)
        {
            if (rightPost > plan1Post[i].position.x - 0.1f && rightPost < plan1Post[i].position.x + 0.1f)
            {
                if (!plan1List.Contains(i))
                {
                    GameObject g = Instantiate(plan1, plan1Post[i].position, Quaternion.identity) as GameObject;
                    plan1List.Add(i);
                    plan1ListGO.Add(g);
                }
                break;
            }
        }

        for (int i = 0; i < plan2Post.Length; i++)
        {
            //Debug.Log("SLUG: " + slugsPost[i].position.x);
            if (rightPost > plan2Post[i].position.x - 0.1f && rightPost < plan2Post[i].position.x + 0.1f)
            {
                if (!plan2List.Contains(i))
                {
                    GameObject g = Instantiate(plan2, plan2Post[i].position, Quaternion.identity) as GameObject;
                    plan2List.Add(i);
                    plan2ListGO.Add(g);
                }
                break;
            }
        }
        for (int i = 0; i < batPost.Length; i++)
        {
            //Debug.Log("SLUG: " + slugsPost[i].position.x);
            if (rightPost > batPost[i].position.x - 0.1f && rightPost < batPost[i].position.x + 0.1f)
            {
                if (!batList.Contains(i))
                {
                    GameObject g = Instantiate(bat, batPost[i].position, Quaternion.identity) as GameObject;
                    batList.Add(i);
                    batListGO.Add(g);
                }
                break;
            }
        }
        for (int i = 0; i < flowerPost.Length; i++)
        {
            //Debug.Log("SLUG: " + slugsPost[i].position.x);
            if (rightPost > flowerPost[i].position.x - 0.1f && rightPost < flowerPost[i].position.x + 0.1f)
            {
                if (!flowerList.Contains(i))
                {
                    GameObject g = Instantiate(flower, flowerPost[i].position, Quaternion.identity) as GameObject;
                    flowerList.Add(i);
                    flowerListGO.Add(g);
                }
                break;
            }
        }
        for (int i = 0; i < rabitPost.Length; i++)
        {
            //Debug.Log("SLUG: " + slugsPost[i].position.x);
            if (rightPost > rabitPost[i].position.x - 0.1f && rightPost < rabitPost[i].position.x + 0.1f)
            {
                if (!rabitList.Contains(i))
                {
                    GameObject g = Instantiate(rabit, rabitPost[i].position, Quaternion.identity) as GameObject;
                    rabitList.Add(i);
                    rabitListGO.Add(g);
                }
                break;
            }
        }
    }

}
