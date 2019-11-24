using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int maxSize = 10;

    private List<GameObject> pooledObjects = new List<GameObject>();

    public GameObject GetObject()
    {
        if (pooledObjects.Count > 0)
        {
            GameObject item = pooledObjects[0];
            pooledObjects.RemoveAt(0);
            item.gameObject.SetActive(true);
            return item;
        }
        return Instantiate(prefab);
    }

    public void ReturnToPool(GameObject item)
    {
        if (pooledObjects.Count < maxSize)
        {
            pooledObjects.Add(item);
            item.transform.SetParent(transform);
            item.gameObject.SetActive(false);
        }
        else
        {
            Destroy(item.gameObject);
        }
    }
}
