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
        DealHandsUp();
        CheckForDefenseSuccess();
        ResetPlayers();
        currentAttacker = GetNextAttacker(defenseSuccessful);
        currentDefender = GetNextDefender();
        players[currentAttacker].isAttacking = true;
        players[currentDefender].isDefending = true;
        players[currentAttacker].GetAlly().isAttacking = true;
        players[currentAttacker].GetHand().MakeHandPlayableForAttack();
        //TODO: test this
    }

    public void ResetPlayers()
    {
        foreach (Player player in players)
        {
            player.isAttacking = false;
            player.isDefending = false;
            player.GetHand().MakeHandUnplayable();
            player.hasEndedTurn = false;
        }
    }

    private void DealHandsUp()
    {
        foreach (Player player in players)
        {
            if (player.GetHand().GetHandSize() < baseHandSize)
            {
                player.DrawCards(baseHandSize - player.GetHand().GetHandSize());
            }
        }
    }

    private void CheckForDefenseSuccess()
    {
        List<Card> cardList = board.GetCardsOnBoard();
        foreach (Card card in cardList)
        {
            if (card.isAttacking && !card.isDefended)
            {
                defenseSuccessful = false;
            }
        }
    }

    private int NextPlayer(int currentPlayer)
    {
        //TODO: Not entirely sure on this, supposed be a circle
        if (currentPlayer + 1 >= players.Length)
        {
            currentPlayer %= players.Length;
        }
        return currentPlayer++;
    }

    private int GetNextAttacker(bool defenseSuccessful)
    {
        if(defenseSuccessful)
        {
            return NextPlayer(currentAttacker);
        }
        else
        {
            if (NextPlayer(currentAttacker) == currentDefender)
            {
                return NextPlayer(NextPlayer(currentAttacker));
            }
            else
            {
                return NextPlayer(currentAttacker);
            }
        }
    }

    private int GetNextDefender()
    {
        if (players[NextPlayer(currentAttacker)] == players[currentAttacker].GetAlly())
        {
            return NextPlayer(NextPlayer(currentAttacker));
        }
        else
        {
            return NextPlayer(currentAttacker);
        }
    }
}
