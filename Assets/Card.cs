using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] int rank;
    [SerializeField] Suit suit;
    
    public string ID;
    public bool isAttacking;
    public bool isDefended;
    public bool canBePlayed;
    public Card defendedByCard;
    public bool onBoard;
    public bool isTrumpSuit;
    public Player cardHolder;
    
    Zone currentZone;
    GameState gameState;

    protected void Awake()
    {
        gameState = FindObjectOfType<GameState>();
    }

    private void Start()
    {
        isTrumpSuit = suit == gameState.GetTrumpSuit();
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

    private void OnTriggerEnter(Collider other)
    {
        onBoard = true;
        print(onBoard);
    }

    private void OnTriggerExit(Collider other)
    {
        onBoard = false;
        print(onBoard);
    }
}
