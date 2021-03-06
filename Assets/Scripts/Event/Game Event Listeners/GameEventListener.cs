using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{
    public interface IGameEventListener
    {
        void OnEventRaised();
    }

    public class GameEventListener : MonoBehaviour, IGameEventListener
    {
        [SerializeField]
        GameEvent gameEvent;
        [SerializeField]
        UnityEvent response;

        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            response.Invoke();
        }
    }
}