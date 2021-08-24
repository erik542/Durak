using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Zone
{
    Board board;

    private new void Awake()
    {
        base.Awake();
        board = FindObjectOfType<Board>();
    }

    public void DiscardCard(Card card)
    {
        if (card.GetCurrentZone() is Board)
        {
            Zone.TransferCard(card, this, board);
            print(card.GetCardName() + " was discarded");
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
