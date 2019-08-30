﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolName
{
    BASE_ENEMY,
    BASE_ENEMY_2,
    SPEAR
}

public class PoolManager : Singleton<PoolManager>
{
    [System.Serializable]
    private struct PoolNameAndPrefab
    {
        public PoolName poolName;
        public GameObject prefab;
    }
    [Header("PoolName and Prefabs")]
    [SerializeField]
    private PoolNameAndPrefab[] poolNameAndPrefab;

    public IDictionary<string, Queue<GameObject>> pools;

    private void Start()
    {
        pools = new Dictionary<string, Queue<GameObject>>();
    }

    public GameObject PopPool(string poolName, Vector2 pos, Quaternion rotate)
    {
        //if(!pools.ContainsKey(poolName))
        //{
        //    //Debug.LogError("Pool can't found!!!!!!!!");
        //    //return null;
        //}
        GameObject obj = null;
        if (pools.ContainsKey(poolName) && pools[poolName].Count > 0)
        {
            obj = pools[poolName].Dequeue();
        }

        if(obj != null)
        {
            obj.SetActive(true);
            obj.transform.position = pos;
            obj.transform.rotation = rotate;
        }
        else
        {
            obj = Instantiate(GetPrefabByName(poolName), pos, rotate) as GameObject;
        }
        obj.transform.SetParent(null);

        return obj;
    }

    public void PushPool(GameObject obj, string poolName)
    {
        if (!pools.ContainsKey(poolName))
            pools.Add(poolName, new Queue<GameObject>());

        if (obj.activeSelf)
        {
            obj.SetActive(false);
        }
        //set parent
        obj.transform.SetParent(transform);
        pools[poolName].Enqueue(obj);


    }

    private GameObject GetPrefabByName(string name)
    {
        for (int i = 0; i < poolNameAndPrefab.Length; i++)
        {
            if (poolNameAndPrefab[i].poolName.ToString().Equals(name))
                return poolNameAndPrefab[i].prefab;
        }

        return null;
    }
}
