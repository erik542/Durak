using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPlayHandler : MonoBehaviour
{
    CardLibrary cardLibrary;

    Dictionary<string, bool> enterPlayListeners;

    private void Awake()
    {
        enterPlayListeners = new Dictionary<string, bool>();
    }

    void Start()
    {
        cardLibrary = FindObjectOfType<CardLibrary>();
    }

    public void AddListener(string listener)
    {
        if (!enterPlayListeners.ContainsKey(listener))
        {
            enterPlayListeners.Add(listener, true);
        }
    }

    public void RemoveListener(string listener)
    {
        if(enterPlayListeners.ContainsKey(listener))
        {
            enterPlayListeners.Remove(listener);
        }
    }

    public void InvokeAllListeners(string publisher)
    {
        foreach (string listener in enterPlayListeners.Keys)
        {
            cardLibrary.GetCardFromLibrary(listener).EnterPlayListener(publisher);
        }
    }
}
