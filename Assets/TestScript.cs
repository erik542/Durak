using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] Card testCard;

    Deck deck;
    Player player;
    Board board;

    private void Awake()
    {
        deck = FindObjectOfType<Deck>();
        player = FindObjectOfType<Player>();
        board = FindObjectOfType<Board>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player.DrawACard();
        player.PlayCard(testCard);
        player.TakeCardsOnBoard();
        player.PlayCard(testCard);
        board.DiscardCardsOnBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
