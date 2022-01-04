using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Zone
{
    
    CardsPile cardsPile;
    Player player;
    int handSize;
    List<Card> playableCards;
    GameState gameState;
    

    private new void Awake()
    {
        base.Awake();
        player = gameObject.GetComponent<Player>();
        cardsPile = GetComponentInChildren<CardsPile>();
        playableCards = new List<Card>();
        gameState = FindObjectOfType<GameState>();
    }

    private void Start()
    {
        RecalcHandSize();
    }

    public new void AddCard(Card card)
    {
        Zone.TransferCard(card, card.GetCurrentZone(),this);
        IncreaseHandSize();
        card.cardHolder = player;
        cardsPile.Add(card.gameObject);
    }

    public void RecalcHandSize()
    {
        handSize = cards.Count;
        player.GetStatusUpdater().UpdateCardInHand(handSize);
    }

    public int GetHandSize()
    {
        return handSize;
    }

    public CardsPile GetCardsPile()
    {
        return cardsPile;
    }

    public bool IsCardInHand(Card card)
    {
        return cards.ContainsKey(card.ID);
    }

    public void IncreaseHandSize()
    {
        handSize++;
        player.GetStatusUpdater().UpdateCardInHand(handSize);
    }

    public void DecreaseHandSize()
    {
        handSize--;
        player.GetStatusUpdater().UpdateCardInHand(handSize);
    }

    public void MakeHandPlayableForAttack()
    {
        foreach (string cardName in cards.Keys)
        {
            MakeCardPlayable(cards[cardName]);
        }
    }

    public void MakeHandUnplayable()
    {
        playableCards.Clear();
        foreach (string cardName in cards.Keys)
        {
            cards[cardName].ToggleCardPlayability(false);
        }
    }

    public void UpdatePlayableCards(List<Card> cardsOnBoard)
    {
        MakeHandUnplayable();
        if (player.isAttacking)
        {
            int undefendedCards = 0;
            foreach (Card cardOnBoard in cardsOnBoard)
            {
                if (cardOnBoard.isAttacking && !cardOnBoard.isDefended)
                {
                    undefendedCards++;
                }
            }
            if (undefendedCards < gameState.GetDefendingPlayer().GetHand().GetHandSize())
            {
                foreach (Card card in cardsOnBoard)
                {
                    foreach (string cardName in cards.Keys)
                    {
                        if (card.GetRank() == cards[cardName].GetRank())
                        {
                            MakeCardPlayable(cards[cardName]);
                        }
                    }
                }
            }
        }
        else if (player.isDefending)
        {
            int undefendedCards = 0;
            foreach (Card cardOnBoard in cardsOnBoard)
            {
                if (cardOnBoard.isAttacking && !cardOnBoard.isDefended)
                {
                    undefendedCards++;
                }
            }
            foreach(Card cardOnBoard in cardsOnBoard)
            {
                if (undefendedCards > 0)
                {
                    foreach (string cardName in cards.Keys)
                    {
                        if (GameState.CheckCardDefense(cards[cardName], cardOnBoard))
                        {
                            MakeCardPlayable(cards[cardName]);
                        }
                    }
                }
            }
        }
    }

    private void MakeCardPlayable(Card card)
    {
        card.ToggleCardPlayability(true);
        playableCards.Add(card);
    }

    public List<Card> GetPlayableCards()
    {
        return playableCards;
    }

    public List<Card> GetCardsInHand()
    {
        List<Card> cardsInHand = new List<Card>();
        if (cards.Count > 0)
        {
            foreach (Card card in cards.Values)
            {
                cardsInHand.Add(card);
            }
        }
        return cardsInHand;
    }
}
