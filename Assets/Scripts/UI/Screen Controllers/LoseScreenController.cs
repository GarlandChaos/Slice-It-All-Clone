using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

namespace UI
{
    public class LoseScreenController : MonoBehaviour
    {
        [SerializeField]
        GameEvent restartLevelEvent = null;

        public void OnRestartButton()
        {
            restartLevelEvent.Invoke();
        }
    }
}
