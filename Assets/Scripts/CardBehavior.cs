using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBehavior : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject dragged;
    public BoxCollider2D collider;
    public CanvasGroup canvasGroup;
    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("Pointer Down");
    }
    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        Debug.Log("DragStart");
        
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        Debug.Log("DragEnd");
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag != null && pointerEventData.pointerDrag.GetComponent<CardStatus>().status == "inHand")
        {
            Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(newPos.x, newPos.y, 80);
        }
    }

    public void EnableRaycast()
    {
        canvasGroup.blocksRaycasts = true;
    }
}
