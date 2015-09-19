using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject canvas;
    // Use this for initialization
    void Start()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EndGame")
        {
            canvas.SetActive(true);
        }
    }
}
