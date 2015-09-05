using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePadController : MonoBehaviour {
    public Button btn_back, btn_forw, btn_jump;
	// Use this for initialization
	void Start ()
    {
        btn_back.onClick.AddListener(() => Movement());
        btn_forw.onClick.AddListener(() => Movement());
        btn_jump.onClick.AddListener(() => Jump());
        bool supportsMultiTouch = Input.multiTouchEnabled;
        Debug.Log("Enable multitouch: "+ supportsMultiTouch);
    }
	
	// Update is called once per frame
	void Update () {
        int touchCount = Input.touchCount;
        if (touchCount > 0)
        {
            Debug.Log(touchCount+" touch(es) detected.");
            for (int i = 0; i < touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                switch (i)
                {
                    case 0:
                        TouchPhase phase = touch.phase;
                        switch (phase)
                        {
                            case TouchPhase.Began:
                                print("New touch detected at position " + touch.position + " , index " + touch.fingerId);
                                break;
                            case TouchPhase.Moved:
                                print("Touch index " + touch.fingerId + " has moved by " + touch.deltaPosition);
                                break;
                            case TouchPhase.Stationary:
                                //PlayerController.Instance.PayerWalk();
                                print("Touch index " + touch.fingerId + " is stationary at position " + touch.position);
                                break;
                            case TouchPhase.Ended:
                                print("Touch index " + touch.fingerId + " ended at position " + touch.position);
                                break;
                            case TouchPhase.Canceled:
                                print("Touch index " + touch.fingerId + " cancelled");
                                break;
                        }
                break;
                    case 1:

                        break;
                    default:
                        break;
                }
            }
        }

	}

    public void Movement()
    {
        PlayerController.Instance.PayerWalk();

    }
    public void Jump()
    {

    }
}
