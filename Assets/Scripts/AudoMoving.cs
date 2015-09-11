using UnityEngine;
using System.Collections;

public class AudoMoving : MonoBehaviour {

    float orient;
    float cameraPost;
    float boundRight, boundLeft;
	// Use this for initialization
	void Start () {
        orient = PlayerController.Instance.gameObject.transform.localScale.x;
        cameraPost = CameraMoving.Instance.gameObject.transform.position.x;
        boundRight = cameraPost + CameraMoving.Instance.sizeOfCamera;
        boundLeft = cameraPost - CameraMoving.Instance.sizeOfCamera;
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.x > boundRight
            || gameObject.transform.position.x < boundLeft)
        {
            Destroy(gameObject);
        }
        gameObject.transform.position += Vector3.right * Time.deltaTime * 2.5f * orient;
    }
}
