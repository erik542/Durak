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
    SpriteRenderer[] cardImageRenderers;

    protected void Awake()
    {
        gameState = FindObjectOfType<GameState>();
        cardSelectionFrameRenderers = GetComponentsInChildren<MeshRenderer>();
        cardImageRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        isTrumpSuit = suit == gameState.GetTrumpSuit();
        ToggleCardHoverState(false);
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
    }

    private void OnTriggerExit(Collider other)
    {
        onBoard = false;
    }

    public void ToggleCardHoverState(bool value)
    {
        ToggleCardHoverImage(value);
        ToggleSelectionFrameRenderers(value);
    }

    private void ToggleCardHoverImage(bool value)
    {
        foreach (SpriteRenderer spriteRenderer in cardImageRenderers)
        {
            if (spriteRenderer.sortingLayerID != 0)
            {
                spriteRenderer.enabled = value;
            }
        }
    }

    private void ToggleSelectionFrameRenderers(bool value)
    {
        foreach (MeshRenderer meshRenderer in cardSelectionFrameRenderers)
        {
            meshRenderer.enabled = value;
        }
    }
}