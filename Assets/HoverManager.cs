using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    private CardSlot cardSlot;
    public bool hasCardSlot = false;
    CardMovementHandler cardMovementHandler;
    private bool hasSubscriber = false;

    public void Subscribe(CardMovementHandler handler)
    {
        cardMovementHandler = handler;
        hasSubscriber = true;
    }

    public void Unsubscribe()
    {
        cardMovementHandler = null;
        hasSubscriber = true;
    }

    public void SetCardSlot(CardSlot cardSlot)
    {
        this.cardSlot = cardSlot;
        hasCardSlot = true;
    }

    public void ClearCardSlot()
    {
        this.cardSlot = null;
        hasCardSlot = false;
        
    }

    public void PublishCardSlot()
    {
        if (hasSubscriber)
        {
            if (hasCardSlot)
            {
                cardMovementHandler.SetActiveCardSlot(cardSlot);
            }
            else
            {
                cardMovementHandler.ClearActiveCardSlot();
            }
        }
    }
}
