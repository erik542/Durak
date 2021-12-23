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

    private void PlayCard(Card card)
    {
        Zone.TransferCard(card, hand, board);
        hand.GetCardsPile().Remove(card.gameObject);
        hand.DecreaseHandSize();
    }

    public void AttackWithCard(Card card)
    {
        if (hand.IsCardInHand(card))
        {
            if (card.canBePlayed)
            {
                PlayCard(card);   
                card.isAttacking = true;
                card.isDefended = false;
                enterPlayHandler.InvokeAllListeners(board);
                card.canBePlayed = false;
                print(card.ID + " was played");
            }
            else
            {
                print(card.ID + " can't be played");
            }
        }
        else
        {
            print(card.ID + " is not in hand");
        }
    }

    public void DefendWithCard(Card cardInHand, Card cardOnBoard)
    {
        if (hand.IsCardInHand(cardInHand))
        {
            if (cardInHand.canBePlayed)
            {
                PlayCard(cardInHand);
                cardOnBoard.isDefended = true;
                cardOnBoard.defendedByCard = cardInHand;
                enterPlayHandler.InvokeAllListeners(board);
                print(cardInHand.ID + " was played");
            }
            else
            {
                print(cardInHand.ID + " cannot be played");
            }
        }
        else
        {
            print(cardInHand.ID + " is not in hand");
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
}
