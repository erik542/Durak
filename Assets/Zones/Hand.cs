using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Zone
{
    Player player;
    int handSize;

    private new void Awake()
    {
        base.Awake();
        player = gameObject.GetComponent<Player>();
    }

    private void Start()
    {
        RecalcHandSize();
    }

    public void RecalcHandSize()
    {
        handSize = cards.Count;
    }

    public int GetHandSize()
    {
        return handSize;
    }
}
