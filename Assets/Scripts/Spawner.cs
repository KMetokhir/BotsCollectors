using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour
    where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    public T Spawn(UnitSpawnPoint spawnPoint)
    {
        T spawnObject = Instantiate(_prefab);
        spawnObject.transform.position = spawnPoint.position;

        return spawnObject;
    }
}