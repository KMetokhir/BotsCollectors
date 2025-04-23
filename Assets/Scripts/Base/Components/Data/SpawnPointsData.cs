using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPointsData : MonoBehaviour
{
    [SerializeField] private List<UnitPoint> _unitSpawnPoints;

    public bool TryGetEmptyPoint(out UnitPoint point)
    {
        point = _unitSpawnPoints.FirstOrDefault(point => point.IsEmpty);

        return point != null;
    }

    public void ClearPoint(Unit unit)
    {
        UnitPoint point = _unitSpawnPoints.FirstOrDefault(point => point.TryGetPosition(unit, out Vector3 position));
        point.Clear();
    }

    public Vector3 GetUnitSpawnPosition(Unit unit)
    {
        Vector3 spawnPosition;

        foreach (UnitPoint point in _unitSpawnPoints)
        {
            if (point.TryGetPosition(unit, out Vector3 position))
            {
                spawnPosition = position;

                return spawnPosition;
            }
        }

        throw new Exception($"Unit {unit.gameObject.name} hasn't position in this Base {gameObject.name}");
    }
}