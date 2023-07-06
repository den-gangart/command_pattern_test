using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoPooledObject : MonoBehaviour
{
    private MonoObjectPool _pool;

    public void Create(MonoObjectPool pool)
    {
        _pool = pool;
        OnCreate();
    }

    public void ReturnToPool()
    {
        _pool.ReturnToPool(this);
    }

    protected virtual void OnCreate() { }
}
