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
        if (player.isAttacking)
        {
            if (hand.GetPlayableCards().Count > 0)
            {
                Card cardToAttackWith = FindCardToAttackWith();
                player.AttackWithCard(cardToAttackWith);
                PlayCardInFirstSlot(cardToAttackWith);
            }
        }
        if (player.isDefending)
        {
            if (hand.GetPlayableCards().Count > 0)
            {

            }
        }
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
        List<float> cardChoiceValue = new List<float>();
        List<Card> playableCards = hand.GetPlayableCards();
        int topPick = 0;
        for (int i = 0; i < playableCards.Count; i++)
        {
            cardChoiceValue.Add(Random.value);
        }
        for (int i = 0; i < cardChoiceValue.Count; i++)
        {
            if (cardChoiceValue[i] >= cardChoiceValue[topPick])
            {
                topPick = i;
            }
        }
        return playableCards[topPick];
    }
}
