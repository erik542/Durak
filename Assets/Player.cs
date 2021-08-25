using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Hand hand;
    Board board;
    Deck deck;
    Player ally;
    bool isAttacking;
    bool isDefending;

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

    public void DrawACard()
    {
        deck.DrawACard(this);
    }

    public void PlayCard(Card card)
    {
        if (hand.IsCardInHand(card))
        {
            Zone.TransferCard(card, hand, board);
            hand.DecreaseHandSize();
            print(card.GetCardName() + " was played");
        }
        else
        {
            print(card.GetCardName() + " is not in hand");
        }
    }

    public void TakeCardsOnBoard()
    {
        List<Card> cardList = board.GetCardsOnBoard();
        foreach (Card card in cardList)
        {
            board.BounceCard(card, this);
            hand.IncreaseHandSize();
        }
    }
}
