using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Zone
{
    
    CardsPile cardsPile;
    Player player;
    int handSize;
    List<Card> playableCards;
    

    private new void Awake()
    {
        base.Awake();
        player = gameObject.GetComponent<Player>();
        cardsPile = GetComponentInChildren<CardsPile>();
        playableCards = new List<Card>();
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
    }

    public void DecreaseHandSize()
    {
        handSize--;
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

    public void UpdatePlayableCards(List<Card> cardList)
    {
        MakeHandUnplayable();
        if (player.isAttacking)
        {
            foreach (Card card in cardList)
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
        else if (player.isDefending)
        {
            foreach (Card card in cardList)
            {
                foreach(string cardName in cards.Keys)
                {
                    if (!card.isDefended && card.isAttacking && (card.GetSuit() == cards[cardName].GetSuit()) && (card.GetRank() < cards[cardName].GetRank() 
                        || (!card.isTrumpSuit && cards[cardName].isTrumpSuit)))
                    {
                        MakeCardPlayable(cards[cardName]);
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
}
