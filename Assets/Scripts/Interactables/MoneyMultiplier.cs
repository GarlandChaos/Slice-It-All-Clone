using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class MoneyMultiplier : MonoBehaviour
{
    [SerializeField]
    uint multiplier = 2;
    [SerializeField]
    GameEvent gameWinEvent = null;
    [SerializeField]
    GameEvent multiplyMoneyEvent = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Knife")
        {
            multiplyMoneyEvent.Invoke(multiplier);
            gameWinEvent.Invoke();
        }
    }
}
