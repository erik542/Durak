using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] int baseHandSize = 6;
    [SerializeField] int initialAttacker = 0;

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
        currentAttacker = initialAttacker;
        currentDefender = GetNextDefender();
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
        int candidate = NextPlayer(currentAttacker);
        bool candidateFail = true;
        if (defenseSuccessful)
        {
            while (candidateFail)
            {
                if (players[candidate].GetHand().GetHandSize() > 0)
                {
                    candidateFail = false;
                }
                else
                {
                    candidate = NextPlayer(candidate);
                }
            }
        }
        else
        {
            while(candidateFail)
            {
                if (candidate != currentDefender || players[candidate].GetHand().GetHandSize() > 0)
                {
                    candidate = NextPlayer(candidate);
                }
                else
                {
                    candidateFail = false;
                }
            }
        }
        return candidate;
    }

    private int GetNextDefender()
    {
        int candidate = NextPlayer(currentAttacker);
        bool candidateFail = true;
        while(candidateFail)
        {
            if (players[currentAttacker].GetAlly() != players[candidate] && players[candidate].GetHand().GetHandSize() > 0)
            {
                candidateFail = false;
            }
            else
            {
                candidate = NextPlayer(candidate);
            }
        }
        return candidate;
    }

    public bool EndTurnChecker()
    {
        bool endTurnFlag = true;
        foreach (Player player in players)
        {
            if (!player.hasEndedTurn)
            {
                endTurnFlag = false;
            }
        }
        return endTurnFlag;
    }
}
