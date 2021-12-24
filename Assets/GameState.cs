using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] int baseHandSize = 3;
    [SerializeField] int initialAttacker = 0;
    [SerializeField] Card testCard;

    int currentDefender;
    int currentAttacker;
    Suit trumpSuit;
    Player[] players;
    Deck deck;
    Board board;
    bool defenseSuccessful = true;

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
        SetTrumpSuit(deck.GetLastCard().GetSuit());
        SetAllies();
        currentAttacker = initialAttacker;
        EndTurn();
        //players[currentAttacker].AttackWithCard(testCard);
    }

    private void SetAllies()
    {
        if (players.Length == 4)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].SetAlly(players[NextPlayer(NextPlayer(i))]);
            }
        }        
    }

    public void EndTurn()
    {
        CheckForDefenseSuccess();
        if (defenseSuccessful)
        {
            board.DiscardCardsOnBoard();
        }
        else
        {
            board.BounceBoard(players[currentDefender]);
        }
        DealHandsUp();
        ResetPlayers();
        if (!CheckForGameEnd())
        {
            currentAttacker = GetNextAttacker(defenseSuccessful);
            currentDefender = GetNextDefender();
            players[currentAttacker].isAttacking = true;
            players[currentDefender].isDefending = true;
            players[currentAttacker].GetAlly().isAttacking = true;
            players[currentAttacker].GetHand().MakeHandPlayableForAttack();
            defenseSuccessful = true;
        }
    }
    
    private bool CheckForGameEnd()
    {
        int playersRemaining = 0;
        foreach (Player player in players)
        {
            if (player.GetHand().GetHandSize() > 0)
            {
                playersRemaining++;
            }
        }
        return playersRemaining <= 1;
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
        currentPlayer++;
        if (currentPlayer >= players.Length)
        {
            currentPlayer %= players.Length;
        }
        return currentPlayer;
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
                if (candidate == currentDefender || players[candidate].GetHand().GetHandSize() == 0)
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

    public bool CheckCardDefense(Card cardInHand, Card cardOnBoard)
    {
        return (cardInHand.GetSuit() == cardOnBoard.GetSuit() && cardInHand.GetRank() > cardOnBoard.GetRank()) || (cardInHand.isTrumpSuit && !cardOnBoard.isTrumpSuit);
    }
}
