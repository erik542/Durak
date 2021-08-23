using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : Zone
{
    LinkedList<Card> cardList;
    int deckSize;

    private new void Awake()
    {
        base.Awake();
        cardList = new LinkedList<Card>();
    }

    private void Start()
    {
        RecalcDeckSize();
    }

    public void Shuffle()
    {
        cardList.Clear();
        //Fisher-Yates Shuffle
        Card[] tempCardList = new Card[base.cards.Count];
        base.cards.Values.CopyTo(tempCardList, 0);
        for (int i = 0; i < tempCardList.Length; i++)
        {
            int j = Random.Range(i, base.cards.Count);
            Card temporary = tempCardList[i];
            tempCardList[i] = tempCardList[j];
            tempCardList[j] = temporary;
        }
        for (int i = 0; i < tempCardList.Length; i++)
        {
            cardList.AddLast(tempCardList[i]);
        }
    }

    public override void AddCard(Card card)
    {
        ShuffleCardIntoDeck(card);
        card.SetCurrentZone(this);
    }

    public void ShuffleCardIntoDeck(Card card)
    {
        base.AddCard(card);
        cardList.AddFirst(card);
        Shuffle();
    }

    public void DrawACard(Player player)
    {
        if (deckSize > 0)
        {
            cardList.First.Value.DrawCard(player);
            cardList.RemoveFirst();
        }
    }

    public void DrawCards(Player player, int cardNumber)
    {
        for (int i = 0; i < cardNumber; i++)
        {
            DrawACard(player);
        }
    }

    public void RecalcDeckSize()
    {
        deckSize = cards.Count;
    }

    public int GetDeckSize()
    {
        return deckSize;
    }
}
