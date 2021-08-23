using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] int rank;
    [SerializeField] Suit suit;
    [SerializeField] string cardName;

    protected Zone currentZone;
    protected Player cardOwner;
    protected Discard discard;

    protected void Awake()
    {
        discard = FindObjectOfType<Discard>();
    }

    public virtual void PlayCard()
    {
        //TODO: a lot.
    }

    public virtual void DrawCard(Player player)
    {
        if (currentZone is Deck)
        {
            Zone.TransferCard(this, currentZone, player.GetHand());
            print(cardName + " was drawn");
        }
        else
        {
            print(cardName + "not in deck");
        }
    }

    public virtual void BounceCard(Player player)
    {
        if (currentZone is Board)
        {
            Zone.TransferCard(this, currentZone, player.GetHand());
            print(cardName + " was bounced");
        }
        else
        {
            print(cardName + " not in play");
        }
    }

    public virtual void DiscardCard()
    {
        if(currentZone is Board)
        {
            Zone.TransferCard(this, currentZone, discard);
            print(cardName + " was destroyed");
        }
        else
        {
            print(cardName + " not in play");
        }
    }

    public string GetCardName()
    {
        return cardName;
    }

    public void SetCardName(string newName)
    {
        cardName = newName;
    }

    public void SetCurrentZone(Zone zone)
    {
        currentZone = zone;
    }

    public Zone GetCurrentZone()
    {
        return currentZone;
    }

    public Suit GetSuit()
    {
        return suit;
    }

    public int GetRank()
    {
        return rank;
    }

    public virtual void EnterPlayListener(string cardID)
    {

    }

    public virtual void LeavePlayListener(string cardID)
    {

    }
}
