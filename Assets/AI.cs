using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    Player player;
    Board board;
    Hand hand;
    [SerializeField] float maxThinkingTime = 3f;
    [SerializeField] float minThinkingTime = .25f;

    private void Awake()
    {
        player = GetComponent<Player>();
        board = FindObjectOfType<Board>();
        hand = GetComponent<Hand>();
    }

    public void Reevaluate()
    {
        List<Card> playableCards = hand.GetPlayableCards();
        if (player.isAttacking)
        {
            if (playableCards.Count > 0)
            {
                Card cardToAttackWith = FindCardToAttackWith();
                PlayCardInFirstSlot(cardToAttackWith);
                player.AttackWithCard(cardToAttackWith);
            }
        }
        if (player.isDefending)
        {
            if (playableCards.Count > 0)
            {
                DefendAgainstBoard(playableCards);
            }
        }
    }

    private void DefendAgainstBoard(List<Card> playableCards)
    {
        CardSlot[] cardSlots = board.GetCardSlots();
        foreach (CardSlot slot in cardSlots)
        {
            if (slot.HasCard() && !slot.IsFull() && playableCards.Count > 0)
            {
                Card cardInSlot = slot.GetCardList()[0];
                List<Card> defendingCards = GetDefendingCards(playableCards, cardInSlot);
                if (defendingCards.Count > 0)
                {
                    Card cardToDefendWith = GetCardToDefendWith(defendingCards);
                    slot.AddCard(cardToDefendWith);
                    player.DefendWithCard(cardToDefendWith, cardInSlot);
                }
            }
        }
    }

    private Card PickRandomCardFromList(List<Card> cards)
    {
        return cards[Random.Range(0, cards.Count - 1)];
    }

    private Card GetCardToDefendWith(List<Card> defendingCards)
    {
        return PickRandomCardFromList(defendingCards);
    }

    private List<Card> GetDefendingCards(List<Card> playableCards, Card attackingCard)
    {
        List<Card> Defendingcards = new List<Card>();
        foreach (Card card in playableCards)
        {
            if (GameState.CheckCardDefense(card, attackingCard))
            {
                Defendingcards.Add(card);
            }
        }
        return Defendingcards;
    }

    private void PlayCardInFirstSlot(Card card)
    {
        CardSlot[] cardSlots = board.GetCardSlots();
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (!cardSlots[i].HasCard())
            {
                cardSlots[i].AddCard(card);
                break;
            }
        }
    }

    private Card FindCardToAttackWith()
    {
        return PickRandomCardFromList(hand.GetPlayableCards());
    }
}
