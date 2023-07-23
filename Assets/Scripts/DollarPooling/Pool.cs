using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool instance;
    public GameObject poolObject;
    private readonly List<GameObject> pooledObjects = new();
    public int poolAmount;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        GameObject tmp;
        for (int i = 0; i < poolAmount; i++)
        {
            tmp = Instantiate(poolObject);
            tmp.transform.rotation = poolObject.transform.rotation;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public void ActivatePoolObject(Vector3 itemTransform)
    {
        GameObject newPoolObject = GetPooledObject();
        if (newPoolObject)
        {
            GetPooledObject().transform.position = itemTransform;
            GetPooledObject().SetActive(true);
        }
    }

    private GameObject GetPooledObject()
    {
        for (int i = 0; i < poolAmount; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
