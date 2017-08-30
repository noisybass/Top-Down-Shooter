using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool<T> where T : MonoBehaviour
{
    private List<T> _pool;

    public Pool(int size, T prefab)
    {
        _pool = new List<T>();
        for (int i = 0; i < size; i++)
        {
            T obj = GameObject.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            _pool.Add(obj);
        }
    }

    public Pool(int size, T prefab, GameObject parent)
    {
        _pool = new List<T>();
        for (int i = 0; i < size; i++)
        {
            T obj = GameObject.Instantiate(prefab);
            obj.transform.SetParent(parent.transform, false);
            obj.gameObject.SetActive(false);
            _pool.Add(obj);
        }
    }

    public T CreateObject()
    {
        if (_pool.Count > 0)
        {
            T obj = _pool[_pool.Count - 1];
            _pool.RemoveAt(_pool.Count - 1);
            return obj;
        }
        return null;
    }

    public void DestroyObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Add(obj);
    }

    public void ClearPool()
    {
        // The GC is supposed to take care of destroying the objects
        _pool.Clear();
        _pool = null;
    }
}
