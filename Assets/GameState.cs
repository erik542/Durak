using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField] int baseHandSize = 20;
    [SerializeField] int initialAttacker = 0;
    [SerializeField] Canvas EndGameCanvas;

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
        EndGameCanvas.enabled = false;
        deck.InitializeDeckComposition();
        SetTrumpSuit(deck.GetLastCard().GetSuit());
        currentAttacker = initialAttacker;
        EndTurn();
    }

    public void TryToEndTurn()
    {
        StartCoroutine(WaitForAIToFinishThinking());
    }

    IEnumerator WaitForAIToFinishThinking()
    {
        yield return new WaitUntil(()=> CheckForAIDoneThinking());
        EndTurn();
    }

    private bool CheckForAIDoneThinking()
    {
        bool doneThinking = true;
        foreach (Player player in players)
        {
            if (player.GetThinkingStatus() && (player.isAttacking || player.isDefending))
            {
                doneThinking = false;
            }
        }
        return doneThinking;
    }

    private void EndTurn()
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
            if (players[currentAttacker].HasAlly())
            {
                players[currentAttacker].GetAlly().isAttacking = true;
            }            
            players[currentAttacker].GetHand().MakeHandPlayableForAttack();
            defenseSuccessful = true;
            if (players[currentAttacker].IsAI())
            {
                StartCoroutine(players[currentAttacker].GetAI().Reevaluate());
            }
        }
        else
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        EndGameCanvas.enabled = true;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
    
    private bool CheckForGameEnd()
    {
        int playersRemaining = 0;
        foreach (Player player in players)
        {
            if (player.HasAlly())
            {
                foreach (Player otherPlayer in players)
                {
                    if (otherPlayer != player.GetAlly() && player.GetHand().GetHandSize() > 0)
                    {
                        playersRemaining++;
                    }
                }
            }
            else if (player.GetHand().GetHandSize() > 0)
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
            if (players[currentAttacker].HasAlly() && players[currentAttacker].GetAlly() != players[candidate] && players[candidate].GetHand().GetHandSize() > 0)
            {
                candidateFail = false;
            }
            else if (players[candidate].GetHand().GetHandSize() > 0)
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

    public static bool CheckCardDefense(Card cardInHand, Card cardOnBoard)
    {
        bool result = (cardInHand.GetSuit() == cardOnBoard.GetSuit() && cardInHand.GetRank() > cardOnBoard.GetRank()) || (cardInHand.isTrumpSuit && !cardOnBoard.isTrumpSuit);
        return result;
    }

    public Player GetDefendingPlayer()
    {
        return players[currentDefender];
    }
}
