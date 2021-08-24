using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Hand hand;
    Board board;
    Deck deck;

    private void Awake()
    {
        hand = GetComponent<Hand>();
        deck = FindObjectOfType<Deck>();
        board = FindObjectOfType<Board>();
    }

    private void Start()
    {

    }

    public Hand GetHand()
    {
        return hand;
    }

    public Board GetField()
    {
        return board;
    }

    public void DrawACard()
    {
        deck.DrawACard(this);
    }

    public void PlayCard(Card card)
    {
        if (hand.IsCardInHand(card))
        {
            Zone.TransferCard(card, hand, board);
            print(card.GetCardName() + " was played");
        }
        else
        {
            print(card.GetCardName() + " is not in hand");
        }
    }

    public void BounceCard(Card card)
    {
        if (board.IsCardOnBoard(card))
        {
            Zone.TransferCard(card, board, hand);
            print(card.GetCardName() + " was bounced");
        }
        else
        {
            print(card.GetCardName() + " was not on board");
        }
    }

    public void TakeCardsOnBoard()
    {
        List<Card> cardList = board.GetCardsOnBoard();
        foreach (Card card in cardList)
        {
            BounceCard(card);
        }
    }
}
