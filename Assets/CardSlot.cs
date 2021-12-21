using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    private HoverManager hoverManager;
    private CardsPile cardsPile;
    private bool hasCard;
    private Card card;

    private void Awake()
    {
        hoverManager = GetComponentInParent<HoverManager>();
        cardsPile = GetComponentInChildren<CardsPile>();
    }

    private void OnMouseEnter()
    {
        hoverManager.SetCardSlot(this);
    }

    private void OnMouseExit()
    {
        hoverManager.ClearCardSlot();
    }

    public CardsPile GetCardsPile()
    {
        return cardsPile;
    }

    public void SetCard(Card newCard)
    {
        card = newCard;
        hasCard = true;
    }

    public void RemoveCard()
    {
        card = null;
        hasCard = false;
    }

    public Card GetCard()
    {
        return card;
    }

    public bool HasCard()
    {
        return hasCard;
    }
}
