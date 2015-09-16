using UnityEngine;
using System.Collections;

public class EnemiBullet : MonoBehaviour {

    Vector3 v;
	// Use this for initialization
	void Start () {
        v = PlayerController.Instance.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += v * Time.deltaTime / 4;
	}
}
