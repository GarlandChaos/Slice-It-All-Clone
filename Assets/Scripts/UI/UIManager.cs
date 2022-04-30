using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameSystem;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        RectTransform winScreen = null;
        [SerializeField]
        RectTransform loseScreen = null;
        [SerializeField]
        RectTransform startScreen = null;
        [SerializeField]
        RectTransform gameplayScreen = null;
        [SerializeField]
        RectTransform configurationsScreen = null;
        [SerializeField]
        RectTransform knifeShopScreen = null;
        [SerializeField]
        TMP_Text moneyPanelText = null;
        public static UIManager instance = null;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            startScreen.gameObject.SetActive(true);
            gameplayScreen.gameObject.SetActive(false);
            winScreen.gameObject.SetActive(false);
            loseScreen.gameObject.SetActive(false);
            configurationsScreen.gameObject.SetActive(false);
            knifeShopScreen.gameObject.SetActive(false);
        }

        public void OpenStartScreen(int level)
        {
            startScreen.gameObject.SetActive(true);
            startScreen.GetComponent<StartScreenController>().SetLevelText(level);
        }

        public void OpenStartScreen()
        {
            startScreen.gameObject.SetActive(true);
        }

        public void CloseStartScreen()
        {
            startScreen.gameObject.SetActive(false);
        }

        public void OpenGameplayScreen()
        {
            gameplayScreen.gameObject.SetActive(true);
        }

        public void CloseGameplayScreen()
        {
            gameplayScreen.gameObject.SetActive(false);
        }

        public void OpenConfigurationsScreen()
        {
            configurationsScreen.gameObject.SetActive(true);
        }

        public void CloseConfigurationsScreen()
        {
            configurationsScreen.gameObject.SetActive(false);
        }

        public void OpenWinScreen()
        {
            winScreen.gameObject.SetActive(true);
        }

        public void CloseWinScreen()
        {
            winScreen.gameObject.SetActive(false);
        }

        public void OpenLoseScreen()
        {
            loseScreen.gameObject.SetActive(true);
        }

        public void CloseLoseScreen()
        {
            loseScreen.gameObject.SetActive(false);
        }

        public void OpenKnifeShopScreen()
        {
            knifeShopScreen.gameObject.SetActive(true);
        }

        public void CloseKnifeShopScreen()
        {
            knifeShopScreen.gameObject.SetActive(false);
        }

        public void ChangeMoneyText()
        {
            moneyPanelText.text = "$ " + GameManager.instance._Money.ToString();
        }
    }
}
