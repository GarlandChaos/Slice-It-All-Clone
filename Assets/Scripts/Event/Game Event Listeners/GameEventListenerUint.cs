using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EventSystem 
{
    public interface IGameEventListenerUint : IGameEventListener
    {
        void OnEventRaised(uint value);
    }

    [System.Serializable]
    public class GameEventUint : UnityEvent<uint> { }

    public class GameEventListenerUint : MonoBehaviour, IGameEventListenerUint
    {
        [SerializeField]
        GameEvent Event;
        [SerializeField]
        GameEventUint Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
#if UNITY_EDITOR
            Debug.Log("Cannot use this version");
#endif
        }

        public void OnEventRaised(uint value)
        {
            Response.Invoke(value);
        }
    }
}
