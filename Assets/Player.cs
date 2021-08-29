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
    GameState gameState;

    private void Awake()
    {
        hand = GetComponent<Hand>();
        deck = FindObjectOfType<Deck>();
        board = FindObjectOfType<Board>();
        enterPlayHandler = FindObjectOfType<EnterPlayHandler>();
        gameState = FindObjectOfType<GameState>();
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

    public void AttackWithCard(Card card)
    {
        if (hand.IsCardInHand(card))
        {
            if (card.canBePlayed)
            {
                Zone.TransferCard(card, hand, board);
                card.isAttacking = true;
                card.isDefended = false;
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

    public void DefendWithCard(Card cardInHand, Card cardOnBoard)
    {
        if (hand.IsCardInHand(cardInHand))
        {
            if (cardInHand.canBePlayed)
            {
                if (CheckCardDefense(cardInHand, cardOnBoard))
                {
                    Zone.TransferCard(cardInHand, hand, board);
                    cardOnBoard.isDefended = true;
                    cardOnBoard.defendedByCard = cardInHand;
                    hand.DecreaseHandSize();
                    enterPlayHandler.InvokeAllListeners(board);
                    print(cardInHand.GetCardName() + " was played");
                }
                else
                {
                    print(cardInHand.GetCardName() + " cannot be played on " + cardOnBoard.GetCardName());
                }
            }
            else
            {
                print(cardInHand.GetCardName() + " cannot be played");
            }
        }
        else
        {
            print(cardInHand.GetCardName() + " is not in hand");
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

    public void EndTurn()
    {
        hasEndedTurn = true;
        if (gameState.EndTurnChecker())
        {
            gameState.EndTurn();
        }
    }

    public bool CheckCardDefense(Card cardInHand, Card cardOnBoard)
    {
        return (cardInHand.GetSuit() == cardOnBoard.GetSuit() && cardInHand.GetRank() > cardOnBoard.GetRank()) || (cardInHand.isTrumpSuit && !cardOnBoard.isTrumpSuit);
    }
}
