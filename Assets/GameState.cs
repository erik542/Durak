using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    Suit trumpSuit;
    Player[] players;
    Deck deck;
    Board board;

    private void Awake()
    {
        players = FindObjectsOfType<Player>();
        deck = FindObjectOfType<Deck>();
        board = FindObjectOfType<Board>();
    }

    private void Start()
    {
        StartGame();
    }

    private void SetTrumpSuit(Suit suit)
    {
        trumpSuit = suit;
    }

    public Suit GetTrumpSuit()
    {
        return trumpSuit;
    }

    private void StartGame()
    {
        foreach (Player player in players)
        {
            player.DrawCards(6);
        }
        SetTrumpSuit(deck.GetLastCard().GetSuit());
    }
}
