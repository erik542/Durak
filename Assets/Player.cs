﻿using System.Collections;
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
    private bool isAI = true;
    AI ai;

    private void Awake()
    {
        hand = GetComponent<Hand>();
        deck = FindObjectOfType<Deck>();
        board = FindObjectOfType<Board>();
        enterPlayHandler = FindObjectOfType<EnterPlayHandler>();
        gameState = FindObjectOfType<GameState>();
        if (GetComponent<AI>() == null)
        {
            isAI = false;
        }
        else
        {
            ai = GetComponent<AI>();
        }
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
            if (card.CanBePlayed())
            {
                PlayCard(card);   
                card.isAttacking = true;
                card.isDefended = false;
                enterPlayHandler.InvokeAllListeners(board);
                card.ToggleCardPlayability(false);
            }
            else
            {
            }
        }
        else
        {
        }
    }

    public void DefendWithCard(Card cardInHand, Card cardOnBoard)
    {
        if (hand.IsCardInHand(cardInHand))
        {
            if (cardInHand.CanBePlayed())
            {
                PlayCard(cardInHand);
                cardOnBoard.isDefended = true;
                cardOnBoard.defendedByCard = cardInHand;
                enterPlayHandler.InvokeAllListeners(board);
            }
            else
            {
            }
        }
        else
        {
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

    public bool IsAI()
    {
        return isAI;
    }

    public AI GetAI()
    {
        return ai;
    }
}
