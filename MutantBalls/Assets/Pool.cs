using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPool
{
    private Dictionary<GameObject, Queue<GameObject>> instances = new Dictionary<GameObject, Queue<GameObject>>();

    public GameObject GetInstance(GameObject prefab)
    {
        var queue = this.GetQueue(prefab);

        GameObject instance;
        if (queue.Count == 0)
        {
            //Debug.Log("new object in pool");
            instance = (GameObject)Object.Instantiate(prefab);
        }
        else
        {
            instance = queue.Dequeue();
        }

        instance.SetActive(true);
        return instance;
    }

    public void Release(GameObject prefab, GameObject instance)
    {
        instance.SetActive(false);
        this.GetQueue(prefab).Enqueue(instance);
    }

    private Queue<GameObject> GetQueue(GameObject prefab)
    {
        if (this.instances.ContainsKey(prefab))
        {
            return this.instances[prefab];
        }

        var queue = new Queue<GameObject>();
        this.instances[prefab] = queue;
        return queue;
    }
}