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
    private Vector3 originalPosition;
    private GameState gameState;
    

    private void Awake()
    {
        card = GetComponent<Card>();
        hoverManager = FindObjectOfType<HoverManager>();
        gameState = FindObjectOfType<GameState>();
    }

    private void Start()
    {
        
    }

    void OnMouseDown()
    {
        if (card.canBePlayed)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            originalPosition = card.gameObject.transform.position;
            offset = originalPosition - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
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
            hoverManager.PublishCardSlot();
            gameObject.layer = 0;
            isMovingSomething = false;
            hoverManager.Unsubscribe();
            if (hasActiveCardSlot)
            {
                if (card.cardHolder.isAttacking && !activeCardSlot.HasCard())
                {
                    card.cardHolder.AttackWithCard(card);
                    activeCardSlot.AddCard(card);
                }
                else if (card.cardHolder.isDefending && activeCardSlot.HasCard() && !activeCardSlot.IsFull() && gameState.CheckCardDefense(card, activeCardSlot.GetCardList()[0]))
                {
                    card.cardHolder.DefendWithCard(card, activeCardSlot.GetCardList()[0]);
                    activeCardSlot.AddCard(card);
                }
                else
                {
                    card.transform.position = originalPosition;
                    OnMouseExit();
                }
            }
            else
            {
                card.transform.position = originalPosition;
                OnMouseExit();
            }
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

    private void OnMouseExit()
    {
        card.ToggleCardHoverState(false);
    }

    private void OnMouseEnter()
    {
        card.ToggleCardHoverState(true);
    }
}
