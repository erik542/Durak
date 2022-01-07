using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Hand hand;
    Board board;
    Deck deck;
    [SerializeField] Player ally;
    private bool hasAlly = false;
    public bool isAttacking;
    public bool isDefending;
    public bool hasEndedTurn;
    EnterPlayHandler enterPlayHandler;
    GameState gameState;
    private bool isAI = true;
    AI ai;
    private bool IsThinking = false;
    StatusUpdater statusUpdater;
    [SerializeField] string playerName;
    AudioSource audioSource;

    private void Awake()
    {
        hand = GetComponent<Hand>();
        deck = FindObjectOfType<Deck>();
        board = FindObjectOfType<Board>();
        enterPlayHandler = FindObjectOfType<EnterPlayHandler>();
        gameState = FindObjectOfType<GameState>();
        statusUpdater = GetComponent<StatusUpdater>();
        if (GetComponent<AI>() == null)
        {
            isAI = false;
        }
        else
        {
            ai = GetComponent<AI>();
        }
        if (ally != null)
        {
            hasAlly = true;
        }
        audioSource = GetComponent<AudioSource>();
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
                audioSource.PlayOneShot(audioSource.clip);
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
                audioSource.PlayOneShot(audioSource.clip);
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

    public void EndTurn()
    {
        hasEndedTurn = true;
        if (gameState.EndTurnChecker())
        {
            gameState.TryToEndTurn();
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

    public bool HasAlly()
    {
        return hasAlly;
    }

    public void UpdateThinkingStatus(bool value)
    {
        IsThinking = value;
        statusUpdater.UpdateThinkingText(value);
    }

    public bool GetThinkingStatus()
    {
        return IsThinking;
    }

    public StatusUpdater GetStatusUpdater()
    {
        return statusUpdater;
    }

    public string GetName()
    {
        return playerName;
    }
}
