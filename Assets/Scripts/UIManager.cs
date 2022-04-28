using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    RectTransform winScreen = null;
    [SerializeField]
    RectTransform loseScreen = null;
    [SerializeField]
    TMP_Text moneyPanelText = null;
    public static UIManager instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        winScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void IncreaseMoneyCount()
    {
        moneyPanelText.text = "$ " + GameManager.instance._Money.ToString();
    }
}
