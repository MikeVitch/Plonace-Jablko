using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Game_Event : ScriptableObject
{
    readonly List<Game_Event_Listener> game_event_listeners = new List<Game_Event_Listener>();
    int i;
    public void Raise()
    {
        for (i = game_event_listeners.Count - 1; i >= 0; i--)
        {
            game_event_listeners[i].OnRaise();
        }
    }

    public void RegisterGameEventListener(Game_Event_Listener listener)
    {
        if (!game_event_listeners.Contains(listener))
        {
            game_event_listeners.Add(listener);
        }
    }

    public void UnregisterGameEventListener(Game_Event_Listener listener)
    {
        if (game_event_listeners.Contains(listener))
        {
            game_event_listeners.Remove(listener);
        }
    }

}