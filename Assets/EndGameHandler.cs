using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameHandler : MonoBehaviour
{
    [SerializeField] Canvas endGameCanvas;
    [SerializeField] Text endGameText;
    [SerializeField] string winningMessage;
    [SerializeField] string losingMessage;

    private void Awake()
    {
    }

    private void Start()
    {
        endGameCanvas.enabled = false;
    }

    public void EndGame(bool playerWin)
    {
        endGameCanvas.enabled = true;
        if (playerWin)
        {
            endGameText.text = winningMessage;
        }
        else
        {
            endGameText.text = losingMessage;
        }
    }
}
