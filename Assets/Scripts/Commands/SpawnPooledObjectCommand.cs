using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPooledObjectCommand : Command
{
    private MonoObjectPool _pool;
    private Vector3 _position;
    private Quaternion _rotation;

    private MonoPooledObject _spawnedObject;

    public SpawnPooledObjectCommand(MonoObjectPool pool, Vector3 startPosition, Quaternion rotation)
    {
        _pool = pool;
        _position = startPosition;
        _rotation = rotation;
    }

    public override void Execute()
    {
        _spawnedObject =  _pool.GetPooledObject<MonoPooledObject>();

        var transform = _spawnedObject.transform;
        transform.position = _position;
        transform.rotation = _rotation;
    }

    public override void Undo() => _spawnedObject.ReturnToPool();
}
