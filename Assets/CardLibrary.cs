using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLibrary : MonoBehaviour
{
    Dictionary<string, Card> cardLibrary;

    private void Awake()
    {
        cardLibrary = new Dictionary<string, Card>();
    }

    public void AddCardToLibrary(Card card)
    {
        cardLibrary.Add(card.name, card);
    }

    public Card GetCardFromLibrary(string cardName)
    {
        if (cardLibrary.ContainsKey(cardName))
        {
            return cardLibrary[cardName];
            //return Instantiate(cardLibrary[cardName]);
        }
        else
        {
            return null;
        }
    }
}
