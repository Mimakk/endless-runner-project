using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance; // Singleton pattern for easy access

    [Header("Pool Settings")]
    public GameObject obstaclePrefab;
    public int poolSize = 20;

    private List<GameObject> pooledObjects;

    void Awake()
    {
        // Singleton safety
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Configuration safety
        if (obstaclePrefab == null)
        {
            Debug.LogError("ObjectPooler: Obstacle Prefab in NOT assigned!");
            enabled = false;
            return;
        }

        // Create pool
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(obstaclePrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        // Search for an unused object in the list
        for (int i=0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
