using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] Card testCard;

    Deck deck;
    Player player;

    private void Awake()
    {
        deck = FindObjectOfType<Deck>();
        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        deck.AddCard(testCard);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
