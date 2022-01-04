using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUpdater : MonoBehaviour
{
    [SerializeField] Text thinkingText;
    [SerializeField] Text cardsInHandText;
    [SerializeField] Text playerNameText;
    private void Awake()
    {
        thinkingText.enabled = false;
    }

    public void UpdateThinkingText(bool value)
    {
        thinkingText.enabled = value;
    }

    public void UpdateCardInHand(int cards)
    {
        cardsInHandText.text = cards.ToString();
    }
}
