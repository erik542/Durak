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

    public void DiscardCardsOnBoard()
    {
        foreach (CardSlot slot in cardSlots)
        {
            DiscardCardsInSlot(slot);
        }
    }

    public void DiscardCardsInSlot(CardSlot slot)
    {
        if (slot.HasCard())
        {
            List<Card> cardList = slot.GetCardList();
            foreach (Card card in cardList)
            {
                if (card.GetCurrentZone() is Board)
                {
                    card.isAttacking = false;
                    card.isDefended = false;
                    card.defendedByCard = null;
                    discard.AddCard(card);
                    print(card.ID + " was discarded");
                }
                else
                {
                    print(card.ID + " is not on board");
                }
            }
            slot.RemoveAllCards();
        }
        else
        {
            //Nothing happens when there is not a card in the slot to discard
        }
        
    }

    public void BounceBoard(Player player)
    {
        foreach (CardSlot slot in cardSlots)
        {
            BounceCardsInSlot(player, slot);
        }
    }

    public void BounceCardsInSlot(Player player, CardSlot slot)
    {
        if (slot.HasCard())
        {
            List<Card> cardList = slot.GetCardList();
            foreach (Card card in cardList)
            {
                if (card.GetCurrentZone() is Board)
                {
                    card.isAttacking = false;
                    card.isDefended = false;
                    card.defendedByCard = null;
                    player.GetHand().AddCard(card);
                    print(card.ID + " was bounced to " + player.name);
                }
                else
                {
                    print(card.ID + " is not on board");
                }
            }
            slot.RemoveAllCards();
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

    public CardSlot[] GetCardSlots()
    {
        return cardSlots;
    }
}
