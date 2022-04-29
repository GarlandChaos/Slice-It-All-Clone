using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScreenController : MonoBehaviour
{
    [SerializeField]
    TMP_Text levelText = null;
    [SerializeField]
    GameEvent firstClickEvent = null;

    public void OnStartButton()
    {
        firstClickEvent.Invoke();
    }

    public void SetLevelText(int level)
    {
        levelText.text = "Nível " + level.ToString();
    }
}
