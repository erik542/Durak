using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Zone
{
    Discard discard;

    private new void Awake()
    {
        base.Awake();
        discard = FindObjectOfType<Discard>();
    }

    public void DiscardCard(Card card)
    {
        if (card.GetCurrentZone() is Board)
        {
            Zone.TransferCard(card, this, discard);
            card.isAttacking = false;
            card.isDefended = false;
            print(card.GetCardName() + " was discarded");
        }
        else
        {
            print(card.GetCardName() + " is not on board");
        }
    }

    public void BounceCard(Card card, Player player)
    {
        if (card.GetCurrentZone() is Board)
        {
            Zone.TransferCard(card, this, player.GetHand());
            card.isAttacking = false;
            card.isDefended = false;
            print(card.GetCardName() + " was bounced to " + player.name);
        }
        else
        {
            print(card.GetCardName() + " is not on board");
        }
    }

    public bool IsCardOnBoard(Card card)
    {
        return cards.ContainsKey(card.GetCardName());
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
