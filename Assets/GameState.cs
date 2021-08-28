using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] int baseHandSize = 6;

    int currentDefender;
    int currentAttacker;
    Suit trumpSuit;
    Player[] players;
    Deck deck;
    Board board;
    bool defenseSuccessful;

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
            player.DrawCards(baseHandSize);
        }
        SetTrumpSuit(deck.GetLastCard().GetSuit());
    }

    public void EndTurn()
    {
        foreach (Player player in players)
        {
            if (player.GetHand().GetHandSize() < baseHandSize)
            {
                player.DrawCards(baseHandSize - player.GetHand().GetHandSize());
            }
        }
        List<Card> cardList = board.GetCardsOnBoard();
        foreach (Card card in cardList)
        {
            if (card.isAttacking && !card.isDefended)
            {
                defenseSuccessful = false;
            }
        }
        if (defenseSuccessful)
        {
            currentAttacker = currentDefender;
        }
        else
        {
            currentAttacker = NextPlayer(currentDefender);
        }

        //TODO: don't buy other stuff
    }

    private int NextPlayer(int currentPlayer)
    {
        if (currentPlayer + 1 >= players.Length)
        {
            currentPlayer %= players.Length;
        }
        return currentPlayer++;
    }
}
