using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Zone
{
    [SerializeField] CardsPile cardsPile;
    
    Player player;
    int handSize;
    

    private new void Awake()
    {
        base.Awake();
        player = gameObject.GetComponent<Player>();
    }

    private void Start()
    {
        RecalcHandSize();
    }

    public override void AddCard(Card card)
    {
        base.AddCard(card);
        cardsPile.Add(Instantiate(card.gameObject));
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
            cards[cardName].canBePlayed = true;
        }
    }

    public void MakeHandUnplayable()
    {
        foreach (string cardName in cards.Keys)
        {
            cards[cardName].canBePlayed = false;
        }
    }

    public void UpdatePlayableCards(List<Card> cardList)
    {
        if (player.isAttacking)
        {
            foreach (Card card in cardList)
            {
                foreach (string cardName in cards.Keys)
                {
                    if (card.GetRank() == cards[cardName].GetRank())
                    {
                        cards[cardName].canBePlayed = true;
                    }
                    else
                    {
                        cards[cardName].canBePlayed = false;
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
                    if ((card.GetSuit() == cards[cardName].GetSuit()) && (card.GetRank() < cards[cardName].GetRank() || (!card.isTrumpSuit && cards[cardName].isTrumpSuit)))
                    {
                        cards[cardName].canBePlayed = true;
                    }
                    else
                    {
                        cards[cardName].canBePlayed = false;
                    }
                }
            }
        }
    }
}
