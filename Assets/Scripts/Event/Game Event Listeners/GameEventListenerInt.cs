using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IGameEventListenerInt : IGameEventListener
{
    void OnEventRaised(int value);
}

[System.Serializable]
public class GameEventInt : UnityEvent<int> { }

public class GameEventListenerInt : MonoBehaviour, IGameEventListenerInt
{
    [SerializeField]
    GameEvent Event;
    [SerializeField]
    GameEventInt Response;

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

    public void OnEventRaised(int value)
    {
        Response.Invoke(value);
    }
}