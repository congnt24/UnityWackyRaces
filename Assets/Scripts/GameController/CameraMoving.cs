using UnityEngine;
using System.Collections;

public class CameraMoving : MonoBehaviour {
	public GameObject player;
    public SpriteRenderer sprite;
    Vector3 startCameraPos, endCameraPos;
    public static CameraMoving Instance;
    public float sizeOfCamera;
    // Use this for initialization
    void Start ()
    {
        sizeOfCamera = gameObject.GetComponent<Camera>().orthographicSize * (float)Screen.width / Screen.height;
        Instance = this;
        //transform.position = Vector3.left * (sprite.bounds.size.x / 2 - gameObject.GetComponent<Camera>().orthographicSize * (float)Screen.width / Screen.height);
        startCameraPos = new Vector3(sprite.bounds.size.x * -1 / 2 + sizeOfCamera, transform.position.y, -10f);
        endCameraPos = new Vector3(sprite.bounds.size.x / 2 - sizeOfCamera, transform.position.y, -10f);
        transform.position = startCameraPos;
    }
	
	// Update is called once per frame
	void Update () {
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
		
	}
}
