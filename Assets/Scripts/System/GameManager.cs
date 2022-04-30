using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UI;
using EventSystem;

namespace GameSystem
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;
        uint money = 0;
        uint levelMoney = 0; //money earned during level to be multiplied at the end
        [SerializeField]
        SceneSettings sceneSettings = null;
        [SerializeField]
        ProductList knivesList = null;
        Dictionary<int, GameObject> knivesGODictionary = new Dictionary<int, GameObject>();
        int levelIterator = 0;
        [SerializeField]
        GameEvent levelLoadedEvent = null;
        [SerializeField]
        GameEvent currentKnifeSet = null;
        int currentKnifeId = 0;
        [SerializeField]
        Transform startTransformReference = null; //start transform reference for the player in each level

        public uint _Money
        {
            get => money;
            set
            {
                money = value;
                UIManager.instance.ChangeMoneyText();
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                if (knivesList)
                {
                    int id = 0;
                    GameObject knifeGO = null;
                    foreach (ProductSettings p in knivesList._Products)
                    {
                        p._ProductID = id;
                        knifeGO = Instantiate(p._ProductGO);
                        knifeGO.transform.SetParent(transform);
                        knifeGO.SetActive(false);
                        knivesGODictionary.Add(id, knifeGO);
                        id++;
                    }
                }
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
            RegisterCurrentKnife(currentKnifeId);
            levelMoney = 0;
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

        public void AddToMoney(uint value)
        {
            _Money += value;
            levelMoney += value;
        }

        public void ReduceFromMoney(uint value)
        {
            _Money -= value;
        }

        public void MultiplyMoney(uint multiplier)
        {
            levelMoney *= multiplier;
            _Money += levelMoney;
        }

        public void RegisterCurrentKnife(int knifeId)
        {
            //deactivate current knife
            knivesGODictionary[currentKnifeId].SetActive(false);

            //set current knife to another knife, set the position and rotation to reference and activate it
            currentKnifeId = knifeId;
            knivesGODictionary[currentKnifeId].transform.position = startTransformReference.position;
            knivesGODictionary[currentKnifeId].transform.rotation = startTransformReference.rotation;
            knivesGODictionary[currentKnifeId].SetActive(true);

            currentKnifeSet.Invoke(knivesGODictionary[currentKnifeId].transform);
        }
    }
}
