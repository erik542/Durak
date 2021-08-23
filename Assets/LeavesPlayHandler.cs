using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavesPlayHandler : MonoBehaviour
{
    CardLibrary cardLibrary;

    Dictionary<string, bool> leavePlayListeners;

    private void Awake()
    {
        leavePlayListeners = new Dictionary<string, bool>();
    }

    void Start()
    {
        cardLibrary = FindObjectOfType<CardLibrary>();
    }

    public void AddListener(string listener)
    {
        if (!leavePlayListeners.ContainsKey(listener))
        {
            leavePlayListeners.Add(listener, true);
        }
    }

    public void RemoveListener(string listener)
    {
        if (leavePlayListeners.ContainsKey(listener))
        {
            leavePlayListeners.Remove(listener);
        }
    }

    public void InvokeAllListeners(string publisher)
    {
        foreach (string listener in leavePlayListeners.Keys)
        {
            cardLibrary.GetCardFromLibrary(listener).LeavePlayListener(publisher);
        }
    }
}
