using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : Zone
{
    [SerializeField] List<Card> initialDeckComposition;
    [SerializeField] CardsPile cardsPile;

    LinkedList<Card> cardList;
    int deckSize;

    private new void Awake()
    {
        base.Awake();
        cardList = new LinkedList<Card>();
    }

    private void Start()
    {
        
    }

    public void Shuffle()
    {
        cardList.Clear();
        //Fisher-Yates Shuffle
        Card[] tempCardList = new Card[cards.Count];
        cards.Values.CopyTo(tempCardList, 0);
        for (int i = 0; i < tempCardList.Length; i++)
        {
            int j = Random.Range(i, cards.Count);
            Card temporary = tempCardList[i];
            tempCardList[i] = tempCardList[j];
            tempCardList[j] = temporary;
        }
        for (int i = 0; i < tempCardList.Length; i++)
        {
            cardList.AddFirst(tempCardList[i]);
        }
    }

    public override void AddCard(Card card)
    {
        base.AddCard(card);
        cardList.AddFirst(card);
        card.SetCurrentZone(this);
        deckSize++;
        cardsPile.Add(card.gameObject, false);
    }

    public void DrawACard(Player player)
    {
        if (deckSize > 0)
        {
            player.GetHand().AddCard(cardList.First.Value);
            cardsPile.Remove(cardList.First.Value.gameObject);
            deckSize--;
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

    public void InitializeDeckComposition()
    {
        foreach (Card card in initialDeckComposition)
        {
            AddCard(card);
        }
        Shuffle();
        RecalcDeckSize();
    }

    public Card GetLastCard()
    {
        return cardList.Last.Value;
    }
}
