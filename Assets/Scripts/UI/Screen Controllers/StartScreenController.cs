using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EventSystem;

namespace UI
{
    public class StartScreenController : MonoBehaviour
    {
        [SerializeField]
        TMP_Text levelText = null;
        [SerializeField]
        GameEvent firstClickEvent = null;
        [SerializeField]
        GameEvent touchedConfigurationsButtonEvent = null;
        [SerializeField]
        GameEvent touchedKnifeShopButtonEvent = null;

        public void OnStartButton()
        {
            firstClickEvent.Invoke();
        }

        public void OnConfigurationsButton()
        {
            touchedConfigurationsButtonEvent.Invoke();
        }

        public void OnKnifeShopButton()
        {
            touchedKnifeShopButtonEvent.Invoke();
        }

        public void SetLevelText(int level)
        {
            levelText.text = "Nível " + level.ToString();
        }
    }
}
