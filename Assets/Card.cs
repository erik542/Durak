using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] int rank;
    [SerializeField] Suit suit;
    [SerializeField] GameObject cardSelectionFrame;
    [SerializeField] Zone currentZone;

    public string ID;
    public bool isAttacking;
    public bool isDefended;
    public bool canBePlayed;
    public Card defendedByCard;
    public bool onBoard;
    public bool isTrumpSuit;
    public Player cardHolder;
    
    GameState gameState;
    MeshRenderer[] cardSelectionFrameRenderers;

    protected void Awake()
    {
        gameState = FindObjectOfType<GameState>();
        cardSelectionFrameRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void Start()
    {
        isTrumpSuit = suit == gameState.GetTrumpSuit();
        ToggleSelectionFrameRenderers(false);
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

    public void ToggleSelectionFrameRenderers(bool value)
    {
        foreach (MeshRenderer meshRenderer in cardSelectionFrameRenderers)
        {
            meshRenderer.enabled = value;
        }
    }
}