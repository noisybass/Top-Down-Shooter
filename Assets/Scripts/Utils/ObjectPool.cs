using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool
{
    private List<GameObject> _pool;

    public ObjectPool(int size, GameObject prefab)
    {
        _pool = new List<GameObject>();
        for (int i = 0; i < size; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab);
            _pool.Add(obj);
        }
    }

    public ObjectPool(int size, GameObject prefab, GameObject parent)
    {
        _pool = new List<GameObject>();
        for (int i = 0; i < size; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.transform.SetParent(parent.transform, false);
            _pool.Add(obj);
        }
    }

    public GameObject CreateObject()
    {
        if (_pool.Count > 0)
        {
            GameObject obj = _pool[_pool.Count - 1];
            _pool.RemoveAt(_pool.Count - 1);
            return obj;
        }
        return null;
    }

    public void DestroyObject(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Add(obj);
    }

    public void ClearPool()
    {
        // The GC is supposed to take care of destroying the objects
        _pool.Clear();
        _pool = null;
    }
}
