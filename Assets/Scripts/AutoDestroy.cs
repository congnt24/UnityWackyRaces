using UnityEngine;
using System.Collections;
using System;

public class AutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(AudoDestroyBang());
	}

    private IEnumerator AudoDestroyBang()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
