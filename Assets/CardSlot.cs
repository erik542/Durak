﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    private HoverManager hoverManager;
    private CardsPile cardsPile;
    private bool hasCard;
    private int cardCount;
    private List<Card> cardList;
    private bool isFull;
    private int maxCards = 2;

    private void Awake()
    {
        hoverManager = GetComponentInParent<HoverManager>();
        cardsPile = GetComponentInChildren<CardsPile>();
        cardList = new List<Card>();
        cardCount = 0;
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

    public void AddCard(Card card)
    {
        cardList.Add(card);
        cardsPile.Add(card.gameObject);
        hasCard = true;
        cardCount++;
        if (cardCount == maxCards)
        {
            isFull = true;
        }
    }

    public void RemoveCard(Card card)
    {
        cardList.Remove(card);
        cardsPile.Remove(card.gameObject);
        cardCount--;
        isFull = false;
        if (cardCount == 0)
        {
            hasCard = false;
        }
    }

    public List<Card> GetCardList()
    {
        return cardList;
    }

    public bool HasCard()
    {
        return hasCard;
    }

    public bool IsFull()
    {
        return isFull;
    }
}
