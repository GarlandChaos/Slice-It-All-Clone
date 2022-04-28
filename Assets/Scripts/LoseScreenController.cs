using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenController : MonoBehaviour
{
    public void OnRestartButton()
    {
        UIManager.instance.CloseLoseScreen();
        GameManager.instance.RestartLevel();
    }
}
