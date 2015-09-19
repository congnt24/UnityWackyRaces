using UnityEngine;
using System.Collections;

public class EnemiBullet : MonoBehaviour {

    Vector3 v;
    float left, right;
	// Use this for initialization
	void Start () {
        v = PlayerController.Instance.transform.position - transform.position;
        left = transform.position.x - CameraMoving.Instance.sizeOfCamera * 2;
        right = transform.position.x + CameraMoving.Instance.sizeOfCamera * 2;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > left && transform.position.x < right)
        {
            transform.position += v * Time.deltaTime / 4;
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
