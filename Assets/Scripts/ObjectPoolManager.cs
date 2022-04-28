using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance = null;
    Dictionary<SliceableModel, ObjectPool> objectPoolDictionary = new Dictionary<SliceableModel, ObjectPool>();

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

    public void AddObjectPool(SliceableModel objectModel, ObjectPool objectPool)
    {
        if (!objectPoolDictionary.ContainsKey(objectModel))
        {
            objectPoolDictionary.Add(objectModel, objectPool);
        }
        else
        {
            Destroy(objectPool.gameObject);
        }
    }

    public void RemoveObjectPool(SliceableModel objectModel)
    {
        if (objectPoolDictionary.ContainsKey(objectModel))
        {
            objectPoolDictionary.Remove(objectModel);
        }
    }

    public GameObject GetObject(SliceableModel objectModel)
    {
        if (objectPoolDictionary.ContainsKey(objectModel))
        {
            return objectPoolDictionary[objectModel].GetObjectFromPool();
        }

        return null;
    }
}
