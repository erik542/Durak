using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUpdater : MonoBehaviour
{
    [SerializeField] Text thinkingText;
    [SerializeField] Text cardsInHandText;
    private void Awake()
    {
        thinkingText.enabled = false;
    }

    public void UpdateThinkingText(bool value)
    {
        thinkingText.enabled = value;
    }

    public void UpdateCardsInHand(int cards, string name)
    {
        cardsInHandText.text = name + " (" + cards.ToString() + ")";
    }
}
