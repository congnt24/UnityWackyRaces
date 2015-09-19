using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class MovingGamePad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private int pointerId;
    private Vector2 direction;
    private bool touched; 
    private Vector2 origin;
    private Vector2 smoothDirection;
    public static MovingGamePad Instance;
    private GameObject parent;
    private Vector2 center;
    // Use this for initialization
    void Awake()
    {
        touched = false;
        direction = Vector2.zero;
    }
    void Start()
    {
        Instance = this;
        parent = gameObject.transform.parent.gameObject;
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("POST: " + origin);
        if (!touched)
        {
            touched = true;
            pointerId = eventData.pointerId;
            origin = eventData.position;
            Vector2 rect = gameObject.GetComponent<RectTransform>().anchoredPosition;

            center = new Vector2(rect.x, rect.y);
            Debug.Log("POST: " + origin);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerId)
        {
            Vector2 currentPosition = eventData.position;
            Vector2 directionRaw = currentPosition - origin;
            direction = directionRaw.normalized;
            Debug.Log("Direction: "+direction+" - "+gameObject.transform.position);
            gameObject.GetComponent<RectTransform>().anchoredPosition = center + direction*50;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerId)
        {
            touched = false;
            direction = Vector2.zero;
            gameObject.GetComponent<RectTransform>().anchoredPosition = center;
        }
    }

    public Vector2 getDirection()
    {
        smoothDirection = Vector2.MoveTowards(smoothDirection, direction, 1);
        //Debug.Log("Smooth+"+smoothDirection);
        return smoothDirection;
    }

}
