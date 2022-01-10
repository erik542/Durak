using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardsPile))]
public class CardSlot : MonoBehaviour
{
    private HoverManager hoverManager;
    private CardsPile cardsPile;
    private bool hasCard;
    private int cardCount = 0;
    private List<Card> cardList;
    private bool isFull;
    private int maxCards = 2;

    [SerializeField] Material slotAvailableMaterial;
    [SerializeField] Material slotUnAvailableMaterial;

    private void Awake()
    {
        hoverManager = GetComponentInParent<HoverManager>();
        cardsPile = GetComponent<CardsPile>();
        cardList = new List<Card>();
    }

    private void OnMouseEnter()
    {
        hoverManager.SetCardSlot(this);
    }

    private void OnMouseExit()
    {
        hoverManager.ClearCardSlot();
    }

    public CardsPile GetCardsPile()
    {
        return cardsPile;
    }

    public void AddCard(Card card)
    {
        cardList.Add(card);
        cardsPile.Add(card.gameObject);
        hasCard = true;
        cardCount++;
        if (cardCount == maxCards)
        {
            isFull = true;
        }
        print(card.ID + " was played in " + gameObject.name);
    }

    public void RemoveCard(Card card)
    {
        cardList.Remove(card);
        cardsPile.Remove(card.gameObject);
        cardCount--;
        isFull = false;
        if (cardCount == 0)
        {
            hasCard = false;
        }
    }

    public List<Card> GetCardList()
    {
        return cardList;
    }

    public bool HasCard()
    {
        return hasCard;
    }

    public bool IsFull()
    {
        return isFull;
    }

    public void RemoveAllCards()
    {
        for (int i = cardList.Count - 1; i >= 0; i--)
        {
            RemoveCard(cardList[i]);
        }
    }
}
