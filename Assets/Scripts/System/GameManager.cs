using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    int money = 0;
    int levelMoney = 0;
    [SerializeField]
    SceneSettings sceneSettings = null;
    int levelIterator = 0;
    [SerializeField]
    GameEvent levelLoadedEvent = null;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelLoadedEvent.Invoke(levelIterator + 1);
    }

    public void LoadNextLevel()
    {
        levelIterator = (levelIterator + 1) % sceneSettings._Scenes.Count;
        SceneManager.LoadScene(sceneSettings._Scenes[levelIterator]);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(levelIterator);
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
