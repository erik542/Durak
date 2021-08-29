using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] int rank;
    [SerializeField] Suit suit;
    [SerializeField] string cardName;

    public bool isAttacking;
    public bool isDefended;
    public bool canBePlayed;
    public Card defendedByCard;

    Zone currentZone;
    public bool isTrumpSuit;
    GameState gameState;

    protected void Awake()
    {
        gameState = FindObjectOfType<GameState>();
    }

    private void Start()
    {
        isTrumpSuit = suit == gameState.GetTrumpSuit();
    }

    public string GetCardName()
    {
        return cardName;
    }

    public void SetCardName(string newName)
    {
        cardName = newName;
    }

    public void SetCurrentZone(Zone zone)
    {
        currentZone = zone;
    }

    public Zone GetCurrentZone()
    {
        return currentZone;
    }

    public Suit GetSuit()
    {
        return suit;
    }

    public int GetRank()
    {
        return rank;
    }
}
