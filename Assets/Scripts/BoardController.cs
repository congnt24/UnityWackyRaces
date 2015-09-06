using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BoardController : MonoBehaviour {

    public Text gemCount, lifesCount, timeCount;
    private float time;
	// Use this for initialization
	void Start () {
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        gemCount.text = string.Format("X {0:00}", PlayerController.Instance.gemCount);
        timeCount.text = ""+(int)(Time.time-time);
        lifesCount.text = string.Format("X {0:00}", PlayerController.Instance.lifeCount);
    }
}
