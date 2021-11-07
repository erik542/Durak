using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    private HoverManager hoverManager;
    public CardsPile cardsPile;

    private void Awake()
    {
        hoverManager = GetComponentInParent<HoverManager>();
        cardsPile = GetComponentInChildren<CardsPile>();
    }

    private void OnMouseEnter()
    {
        hoverManager.SetCardSlot(this);
    }

    private void OnMouseExit()
    {
        hoverManager.ClearCardSlot();
    }
}
