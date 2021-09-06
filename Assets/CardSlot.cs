using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    private HoverManager hoverManager;

    private void Awake()
    {
        hoverManager = GetComponentInParent<HoverManager>();
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
