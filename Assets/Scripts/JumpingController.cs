using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class JumpingController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool touched;
    private int pointerId;
    private bool canJump;
    public static JumpingController Instance;
    private float nextJump;

    public void Start()
    {
        Instance = this;
        touched = false;
        canJump = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched)
        {
            touched = true;
            pointerId = eventData.pointerId;
            canJump = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (pointerId == eventData.pointerId)
        {
            touched = false;
            canJump = false;
        }
    }

    public bool getBoolJump()
    {
        if (Time.time > nextJump)
        {
            nextJump = Time.time + 0.1f;
            return canJump;
        }
        return false;
    }
}
