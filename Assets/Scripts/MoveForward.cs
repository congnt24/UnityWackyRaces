using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class MoveForward : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    int iForward=0, iBack;
    private float moveForward;
    private bool canMove;

    public static MoveForward Instance;
    // Use this for initialization
    void Start()
    {
        canMove = false;
        moveForward = 0;
        Instance = this;
        iBack = 0;
    }

    public void Update()
    {

        if (canMove)
        {
            if (moveForward >= 1)
            {
                iForward = 0;
            }
            moveForward = Mathf.MoveTowards(moveForward, 1, (iForward++)/4 * Time.deltaTime);
        }
        else
        {
            if (moveForward > 0)
            {
                moveForward = Mathf.MoveTowards(moveForward, 0, (iBack++)/4 * Time.deltaTime);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canMove = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        iForward = 0;
        iBack = 0;
        canMove = false;
    }
    public float getDirection()
    {
        return moveForward;
    }
    
}
