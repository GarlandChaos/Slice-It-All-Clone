using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
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

                IGameEventListenerUint listenerUint = listeners[i] as IGameEventListenerUint;
                if (listenerUint != null)
                {
                    listenerUint.OnEventRaised((uint)args[0]);
                    continue;
                }

                IGameEventListenerTransform listenerTransform = listeners[i] as IGameEventListenerTransform;
                if (listenerTransform != null)
                {
                    listenerTransform.OnEventRaised((Transform)args[0]);
                    continue;
                }

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
}