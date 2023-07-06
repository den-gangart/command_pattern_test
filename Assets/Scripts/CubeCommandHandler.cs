using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCommandHandler
{
    public event Action<Command> CubeSpawned;
    public event Action<Command> CubeColored;

    private Camera _camera;
    private Cube _cubePrefab;
    private GameField _gameField;

    private MonoObjectPool _pool;
    private const int DEFAULT_POOL_CAPACITY = 50;
    private const string POOL_NAME = "Cube pool";

    public CubeCommandHandler(Cube prefab, GameField gameField)
    {
        _cubePrefab = prefab;
        _gameField = gameField;
        _camera = Camera.main;

        _pool = new MonoObjectPool(DEFAULT_POOL_CAPACITY, _cubePrefab, POOL_NAME);
    }

    public void TrySpawnCube()
    {
        if (!RaycastFromSreen(out RaycastHit hit) || !_gameField.isPositionInSpawnZone(hit.point))
        {
            return;
        }

        var command = new SpawnPooledObjectCommand(_pool, hit.point + Vector3.up, Quaternion.identity); 

        command.Execute();

        CubeSpawned?.Invoke(command);
    }

    public void TryPaintCube()
    {
        if (!RaycastFromSreen(out RaycastHit hit))
        {
            return;
        }

        if (hit.transform.TryGetComponent(out Cube cube))
        {
            var command = new PaintCubeCommand(cube);
            command.Execute();
            CubeColored?.Invoke(command);
        }
    }

    private bool RaycastFromSreen(out RaycastHit hit)
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.farClipPlane));

        if (Physics.Raycast(ray, out RaycastHit currentHit))
        {
            hit = currentHit;
            return true;
        }

        hit = default;
        return false;
    }
}
