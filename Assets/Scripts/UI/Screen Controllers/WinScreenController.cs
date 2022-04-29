using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenController : MonoBehaviour
{
    [SerializeField]
    GameEvent loadNextLevelEvent = null;

    public void OnNextButton()
    {
        loadNextLevelEvent.Invoke();
    }
}
