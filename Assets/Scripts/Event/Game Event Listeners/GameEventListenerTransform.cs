using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{
    public interface IGameEventListenerTransform : IGameEventListener
    {
        void OnEventRaised(Transform value);
    }

    [System.Serializable]
    public class GameEventTransform : UnityEvent<Transform> { }

    public class GameEventListenerTransform : MonoBehaviour, IGameEventListenerTransform
    {
        [SerializeField]
        GameEvent Event;
        [SerializeField]
        GameEventTransform Response;

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

        public void OnEventRaised(Transform value)
        {
            Response.Invoke(value);
        }
    }
}
