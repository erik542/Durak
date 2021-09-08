using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone: MonoBehaviour
{
    protected Dictionary<string, Card> cards;

    protected void Awake()
    {
        cards = new Dictionary<string, Card>();
    }
    public static void TransferCard(Card card, Zone startZone, Zone endZone)
    {
        startZone.RemoveCard(card);
        endZone.AddCard(card);
        card.SetCurrentZone(endZone);
    }

    public virtual void AddCard(Card card)
    {
        cards.Add(card.ID,card);
    }

    public virtual void RemoveCard(Card card)
    {
        cards.Remove(card.ID);
    }
}
