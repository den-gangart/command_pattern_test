using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoObjectPool
{
    private List<MonoPooledObject> _innactiveObjectList;
    private List<MonoPooledObject> _activeObjectList;
    private int _capacity;

    public MonoObjectPool(int capacity, MonoPooledObject prefab, string poolName)
    {
        _innactiveObjectList = new List<MonoPooledObject>(capacity);
        _activeObjectList = new List<MonoPooledObject>(capacity);

        GameObject poolParent = new GameObject(poolName);

        for(int i = 0; i < capacity; i++)
        {
            var pooledGO = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, poolParent.transform);

            pooledGO.Create(this);

            pooledGO.gameObject.SetActive(false);
            _innactiveObjectList.Add(pooledGO);
        }
    }

    public T GetPooledObject<T>()
    {
        MonoPooledObject pooledObject;

        if(_innactiveObjectList.Count == 0)
        {
            pooledObject = _activeObjectList[0];
            pooledObject.ReturnToPool();
        }

        pooledObject = _innactiveObjectList[_innactiveObjectList.Count - 1];
        pooledObject.gameObject.SetActive(true);

        _innactiveObjectList.Remove(pooledObject);
        _activeObjectList.Add(pooledObject);

        return pooledObject.GetComponent<T>();
    }

    public void ReturnToPool(MonoPooledObject pooledObject)
    {
        _activeObjectList.Remove(pooledObject);
        _innactiveObjectList.Add(pooledObject);

        pooledObject.gameObject.SetActive(false);
    }
}
