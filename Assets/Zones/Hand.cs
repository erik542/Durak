using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Zone
{
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

    public void RecalcHandSize()
    {
        handSize = cards.Count;
    }

    public int GetHandSize()
    {
        return handSize;
    }

    public bool IsCardInHand(Card card)
    {
        return cards.ContainsKey(card.GetCardName());
    }

    public void IncreaseHandSize()
    {
        handSize++;
    }

    public void DecreaseHandSize()
    {
        handSize--;
    }

    public void UpdatePlayableCards(List<Card> cardList)
    {
        if (player.isAttacking)
        {

        }
        else if (player.isDefending)
        {
            foreach (Card card in cardList)
            {
                foreach(string cardName in cards.Keys)
                {
                    if ((card.GetSuit() == cards[cardName].GetSuit()) && (card.GetRank() < cards[cardName].GetRank()))
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
