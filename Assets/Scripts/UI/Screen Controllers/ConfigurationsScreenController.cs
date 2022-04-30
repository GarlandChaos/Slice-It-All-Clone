using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

namespace UI
{
    public class ConfigurationsScreenController : MonoBehaviour
    {
        [SerializeField]
        GameEvent closeConfigurationsScreenEvent = null;

        public void OnCloseButton()
        {
            closeConfigurationsScreenEvent.Invoke();
            gameObject.SetActive(false);
        }
    }
}