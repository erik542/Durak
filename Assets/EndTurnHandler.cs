using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnHandler : MonoBehaviour
{
    GameState gameState;
    Player[] players;

    private void Awake()
    {
        players = FindObjectsOfType<Player>();
    }

    //TODO: how do I do this?
}
