using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class AttackingController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool touched;
    private int pointerId;
    private bool canAttack;
    public static AttackingController Instance;
    private float nextAttack;

    public void Start()
    {
        Instance = this;
        touched = false;
        canAttack = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched)
        {
            touched = true;
            pointerId = eventData.pointerId;
            canAttack = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (pointerId == eventData.pointerId)
        {
            touched = false;
            canAttack = false;
        }
    }

    public bool getBoolAttack()
    {
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + 0.1f;
            return canAttack;
        }
        return false;
    }
}
