using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBehavior : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject dragged;
    public GameObject deckList;
    public BoxCollider2D collider;
    public CanvasGroup canvasGroup;
    public Card card;
    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        canvasGroup = GetComponent<CanvasGroup>();
        deckList = GameObject.Find("DeckList");
    }
    private void Start()
    {
        card = GetComponent<CardDisplay>().card;
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (card.status == "deckBuild")
        {
            card.player.deck.Add(card);
            deckList.GetComponent<DeckListDisplay>().DisplayDeckList();

        }    
    }
    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        //Debug.Log("DragStart");
        
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag != null && pointerEventData.pointerDrag.GetComponent<CardDisplay>().card.status == "inHand")
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
