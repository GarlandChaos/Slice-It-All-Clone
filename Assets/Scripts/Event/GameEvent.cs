using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Game Event")]
public class GameEvent : ScriptableObject
{
    List<IGameEventListener> listeners = new List<IGameEventListener>();

    public void Invoke(params object[] args)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            IGameEventListenerInt listenerInt = listeners[i] as IGameEventListenerInt;
            if (listenerInt != null)
            {
                listenerInt.OnEventRaised((int)args[0]);
                continue;
            }

            //IGameEventListenerFloat listenerFloat = listeners[i] as IGameEventListenerFloat;
            //if (listenerFloat != null)
            //{
            //    listenerFloat.OnEventRaised((float)args[0]);
            //    continue;
            //}

            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(IGameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(IGameEventListener listener)
    {
        listeners.Remove(listener);
    }
}