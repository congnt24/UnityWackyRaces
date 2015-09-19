using UnityEngine;
using System.Collections;
using System;

public class AutoDestroy : MonoBehaviour {
    float size= 0.15f;
	// Use this for initialization
	void Start () {
        StartCoroutine(AudoDestroyBang());
    }

    private IEnumerator AudoDestroyBang()
    {
        yield return new WaitForSeconds(0.05f);
        transform.position += Vector3.left * size;
        yield return new WaitForSeconds(0.05f);
        transform.position += Vector3.up * size;
        yield return new WaitForSeconds(0.05f);
        transform.position += Vector3.right * size;
        yield return new WaitForSeconds(0.05f);
        transform.position += Vector3.down * size;
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }
    public void anim1()
    {

    }
}
