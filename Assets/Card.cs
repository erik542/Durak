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

    Zone currentZone;
    Discard discard;
    Board board;
    bool isTrumpSuit;
    GameState gameState;

    protected void Awake()
    {
        discard = FindObjectOfType<Discard>();
        board = FindObjectOfType<Board>();
        gameState = FindObjectOfType<GameState>();
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

    public virtual void EnterPlayListener(string cardID)
    {

    }

    public virtual void LeavePlayListener(string cardID)
    {

    }
}
