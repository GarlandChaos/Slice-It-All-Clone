using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScreenController : MonoBehaviour
{
    [SerializeField]
    GameEvent touchedGameplayScreenEvent = null;
    [SerializeField]
    GameEvent restartLevelEvent = null;

    public void OnTouchScreen()
    {
        touchedGameplayScreenEvent.Invoke();
    }

    public void OnRestartLevel()
    {
        restartLevelEvent.Invoke();
    }
}
