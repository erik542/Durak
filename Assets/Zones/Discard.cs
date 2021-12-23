using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discard : Zone
{
    private CardsPile cardsPile;

    private new void Awake()
    {
        base.Awake();
        cardsPile = GetComponentInChildren<CardsPile>();
    }

    public CardsPile GetCardsPile()
    {
        return cardsPile;
    }

    public new void AddCard(Card card)
    {
        Zone.TransferCard(card, card.GetCurrentZone(), this);
        cardsPile.Add(card.gameObject);
    }
}
