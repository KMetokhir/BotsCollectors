using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour
    where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    public T Spawn(Vector3 spawnPoint)
    {
        T spawnObject = Instantiate(_prefab);
        spawnObject.transform.position = spawnPoint;

        return spawnObject;
    }
}