using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPlayHandler : MonoBehaviour
{
    Dictionary<Player, bool> enterPlayListeners;

    private void Awake()
    {
        enterPlayListeners = new Dictionary<Player, bool>();
    }

    void Start()
    {
    }

    public void AddListener(Player listener)
    {
        if (!enterPlayListeners.ContainsKey(listener))
        {
            enterPlayListeners.Add(listener, true);
        }
    }

    public void RemoveListener(Player listener)
    {
        if(enterPlayListeners.ContainsKey(listener))
        {
            enterPlayListeners.Remove(listener);
        }
    }

    public void InvokeAllListeners(Board board)
    {
        foreach (Player listener in enterPlayListeners.Keys)
        {
            listener.GetHand().UpdatePlayableCards(board.GetCardsOnBoard());
            if (listener.IsAI())
            {
                listener.GetAI().Reevaluate();
            }
        }
    }
}
