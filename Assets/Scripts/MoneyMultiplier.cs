using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMultiplier : MonoBehaviour
{
    [SerializeField]
    int multiplier = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Knife")
        {
            GameManager.instance.MultiplyMoney(multiplier);
            UIManager.instance.OpenWinScreen();
        }
    }
}