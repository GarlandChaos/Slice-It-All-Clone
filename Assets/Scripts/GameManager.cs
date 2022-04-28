using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //escrever core loop
    //tornar kinematic quando toca nos pilares
    public static GameManager instance = null;
    int money = 0;
    int levelMoney = 0;

    public int _Money 
    { 
        get => money; 
        set 
        {
            money = value;
            UIManager.instance.IncreaseMoneyCount();
        } 
    }

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        //load next level
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddToMoney(int value)
    {
        _Money += value;
    }

    public void ReduceFromMoney(int value)
    {
        _Money -= value;
    }

    public void MultiplyMoney(int multiplier)
    {
        _Money *= multiplier;
    }
}
