using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Zone
{
    Discard discard;
    CardSlot[] cardSlots;

    private new void Awake()
    {
        base.Awake();
        discard = FindObjectOfType<Discard>();
        cardSlots = GetComponentsInChildren<CardSlot>();
    }

    public void DiscardCard(Card card)
    {
        if (card.GetCurrentZone() is Board)
        {
            Zone.TransferCard(card, this, discard);
            card.isAttacking = false;
            card.isDefended = false;
            card.defendedByCard = null;
            print(card.ID + " was discarded");
        }
        else
        {
            print(card.ID + " is not on board");
        }
    }

    public void BounceBoard(Player player)
    {
        foreach (CardSlot slot in cardSlots)
        {
            BounceCard(player, slot);
        }
    }

    public void BounceCard(Player player, CardSlot slot)
    {
        if (slot.HasCard())
        {
            Card card = slot.GetCard();
            if (card.GetCurrentZone() is Board)
            {
                Zone.TransferCard(card, this, player.GetHand());
                card.isAttacking = false;
                card.isDefended = false;
                card.defendedByCard = null;
                slot.GetCardsPile().Remove(card.gameObject);
                player.GetHand().GetCardsPile().Add(card.gameObject);
                print(card.ID + " was bounced to " + player.name);
            }
            else
            {
                print(card.ID + " is not on board");
            }
        }
        else
        {
            print(slot.name + " does not have a card to bounce");
        }
    }

    public bool IsCardOnBoard(Card card)
    {
        return cards.ContainsKey(card.ID);
    }

    public List<Card> GetCardsOnBoard()
    {
        List<Card> cardList = new List<Card>();
        foreach (Card card in cards.Values)
        {
            cardList.Add(card);
        }
        return cardList;
    }

    public void DiscardCardsOnBoard()
    {
        List<Card> cardList = GetCardsOnBoard();
        foreach (Card card in cardList)
        {
            DiscardCard(card);
        }
    }
}
