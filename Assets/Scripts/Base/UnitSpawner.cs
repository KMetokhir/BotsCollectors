using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;  

    public Unit Spawn(UnitSpawnPoint spawnPoint)
    {
        Unit unit = Instantiate(_unitPrefab);
        unit.transform.position =spawnPoint.position;

        return unit;
    }
}
