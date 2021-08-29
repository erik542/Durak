using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Hand hand;
    Board board;
    Deck deck;
    Player ally;
    public bool isAttacking;
    public bool isDefending;
    public bool hasEndedTurn;
    EnterPlayHandler enterPlayHandler;

    private void Awake()
    {
        hand = GetComponent<Hand>();
        deck = FindObjectOfType<Deck>();
        board = FindObjectOfType<Board>();
        enterPlayHandler = FindObjectOfType<EnterPlayHandler>();
    }

    private void Start()
    {
        enterPlayHandler.AddListener(this);
    }

    public Hand GetHand()
    {
        return hand;
    }

    public void DrawACard()
    {
        deck.DrawACard(this);
    }

    public void DrawCards(int number)
    {
        for (int i = 0; i < number; i++)
        {
            DrawACard();
        }
    }

    public void PlayCard(Card card)
    {
        if (hand.IsCardInHand(card))
        {
            if (card.canBePlayed)
            {
                Zone.TransferCard(card, hand, board);
                hand.DecreaseHandSize();
                enterPlayHandler.InvokeAllListeners(board);
                print(card.GetCardName() + " was played");
            }
            else
            {
                print(card.GetCardName() + " can't be played");
            }
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

    public Player GetAlly()
    {
        return ally;
    }

    public void SetAlly(Player player)
    {
        ally = player;
    }
}
