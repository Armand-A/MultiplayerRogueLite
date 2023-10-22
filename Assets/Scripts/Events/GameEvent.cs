/**
 * Credit to Ryan Hipple, Unite Austin 2017 - Game Architecture with Scriptable Objects
 */

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener)) return;
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener)) return;
        listeners.Remove(listener);
    }
}


