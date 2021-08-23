using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Hand hand;
    Discard discard;
    Board board;
    Deck deck;

    private void Awake()
    {
        hand = GetComponent<Hand>();
        deck = FindObjectOfType<Deck>();
        discard = FindObjectOfType<Discard>();
        board = FindObjectOfType<Board>();
    }

    private void Start()
    {

    }

    public Hand GetHand()
    {
        return hand;
    }

    public Discard GetDiscard()
    {
        return discard;
    }

    public Board GetField()
    {
        return board;
    }

    public void AddCardToDeck(Card card)
    {
        deck.AddCard(card);
    }

    public void DrawACard()
    {
        deck.DrawACard(this);
    }
}
