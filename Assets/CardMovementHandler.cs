using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovementHandler : MonoBehaviour
{
    private Card card;
    private Vector3 screenPoint;
    private Vector3 offset;

    private CardSlot activeCardSlot;
    private HoverManager hoverManager;
    public bool hasActiveCardSlot;
    public bool isMovingSomething;

    private void Awake()
    {
        card = GetComponent<Card>();
        hoverManager = FindObjectOfType<HoverManager>();
    }

    private void Start()
    {
        
    }

    void OnMouseDown()
    {
        if (card.canBePlayed)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            gameObject.layer = 2;
            hoverManager.Subscribe(this);
            isMovingSomething = true;
        }
        else
        {
            print("can't be played");
        }
    }

    void OnMouseDrag()
    {
        if(isMovingSomething)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }        
    }

    private void OnMouseUp()
    {
        if(isMovingSomething)
        {
            //card.cardHolder.AttackWithCard(card);
            hoverManager.PublishCardSlot();
            card.transform.parent = activeCardSlot.transform;
            gameObject.layer = 0;
            isMovingSomething = false;
            hoverManager.Unsubscribe();
        }
    }

    public void SetActiveCardSlot(CardSlot cardSlot)
    {
        activeCardSlot = cardSlot;
        hasActiveCardSlot = true;
    }

    public void ClearActiveCardSlot()
    {
        activeCardSlot = null;
        hasActiveCardSlot = false;
    }
}
