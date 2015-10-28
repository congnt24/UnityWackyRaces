using UnityEngine;
using System.Collections;

public class AutoGenerate : MonoBehaviour
{
    public GameObject slug, plan1, plan2, bat, flower, rabit;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(CameraMoving.Instance.transform.position.x + CameraMoving.Instance.sizeOfCamera, 0, 0);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (PlayerController.Instance.transform.localScale.x == 1 && PlayerController.Instance.moveH > 0.05f)
        {

            if (collision.gameObject.tag == "SlugPost")
            {
                generateMonster(slug, collision);
            }
            if (collision.gameObject.tag == "Plan1Post")
            {
                generateMonster(plan1, collision);
            }
            if (collision.gameObject.tag == "Plan2Post")
            {
                generateMonster(plan2, collision);
            }
            if (collision.gameObject.tag == "BatPost")
            {
                generateMonster(bat, collision);
            }
            if (collision.gameObject.tag == "FlowerPost")
            {
                generateMonster(flower, collision);
            }
            if (collision.gameObject.tag == "RabitPost")
            {
                generateMonster(rabit, collision);
            }
        }
    }
    
    public void generateMonster(GameObject monster, Collider2D collision)
    {

        GameObject g = Instantiate(monster, collision.transform.position, Quaternion.identity) as GameObject;
        g.GetComponent<SlugsController>().orgin = collision.gameObject;
        collision.gameObject.SetActive(false);

    }

}
