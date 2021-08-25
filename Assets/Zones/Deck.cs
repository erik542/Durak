using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : Zone
{
    [SerializeField] List<Card> initialDeckComposition;

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
        InitializeDeckComposition();
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
        base.AddCard(card);
        cardList.AddFirst(card);
        card.SetCurrentZone(this);
        deckSize++;
    }

    public void DrawACard(Player player)
    {
        if (deckSize > 0)
        {
            Zone.TransferCard(cardList.First.Value, this, player.GetHand());
            player.GetHand().IncreaseHandSize();
            cardList.RemoveFirst();
            deckSize--;
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

    private void InitializeDeckComposition()
    {
        foreach (Card card in initialDeckComposition)
        {
            AddCard(card);
        }
        Shuffle();
    }
}
