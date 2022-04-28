using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenController : MonoBehaviour
{
    public void OnNextButton()
    {
        UIManager.instance.CloseWinScreen();
    }
}
