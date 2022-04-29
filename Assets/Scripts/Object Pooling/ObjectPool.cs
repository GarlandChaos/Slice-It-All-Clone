using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField]
    GameObject objectToPool = null;
    [SerializeField]
    int numberOfObjectsToPool = 0;
    SliceableModel objectModel = SliceableModel.sphere;

    private void Awake()
    {
        GameObject temporaryGO = null;
        for(int i = 0; i < numberOfObjectsToPool; i++)
        {
            pooledObjects.Add(InstantiateGameObject());
        }
    }

    private void Start()
    {
        if(ObjectPoolManager.instance != null)
        {
            ObjectPoolManager.instance.AddObjectPool(objectModel, this);
        }
    }

    private void OnValidate()
    {
        if(objectToPool != null)
        {
            objectModel = objectToPool.GetComponent<Sliceable>()._Model;
        }
    }

    GameObject InstantiateGameObject()
    {
        GameObject temporaryGO = Instantiate(objectToPool);
        temporaryGO.SetActive(false);
        temporaryGO.transform.SetParent(transform);

        return temporaryGO;
    }

    public GameObject GetObjectFromPool()
    {
        foreach(GameObject go in pooledObjects)
        {
            if (!go.activeInHierarchy)
            {
                return go;
            }
        }

        //must instantiate a new object, add to pool and return it
        GameObject temporaryGO = InstantiateGameObject();
        pooledObjects.Add(temporaryGO);
        return temporaryGO; 
    }
}
