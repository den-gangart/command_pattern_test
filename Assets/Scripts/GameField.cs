using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField] private Transform _spawnZoneCenter;
    [SerializeField] private Vector2 _fieldSize;

    public bool isPositionInSpawnZone(Vector3 position)
    {
        Vector3 center = _spawnZoneCenter.position;

        bool isHorizontalInZone = Mathf.Abs(position.x) < center.x + _fieldSize.x / 2;
        bool isVerticalInZone = Mathf.Abs(position.z) < center.z + _fieldSize.y / 2;

        return isHorizontalInZone && isVerticalInZone;
    }
}
