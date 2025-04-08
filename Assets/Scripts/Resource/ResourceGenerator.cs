using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : Spawner<Resource>
{
    [SerializeField] private List<Transform> _spawnPositions;
    [SerializeField] private Vector2 _spawnDelay;

    private Coroutine _spawnCoroutine;

    private void OnValidate()
    {
        _spawnDelay = new Vector2(Mathf.Abs(_spawnDelay.x), Mathf.Abs(_spawnDelay.y));
    }
    private void OnDisable()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    public void Generate()
    {
        if (_spawnCoroutine != null)
        {
            return;
        }

        _spawnCoroutine = StartCoroutine(GenerateResources());
    }

    private IEnumerator GenerateResources()
    {
        int firstIndex = 0;
        int lastIndex = _spawnPositions.Count - 1;

        while (enabled)
        {
            float delay = Random.Range(_spawnDelay.x, _spawnDelay.y);
            int randomIndex = Random.Range(firstIndex, lastIndex);
            Vector3 randomPoint = _spawnPositions[randomIndex].position;

            Spawn(randomPoint);

            WaitForSeconds wait = new WaitForSeconds(delay);

            yield return wait;
        }
    }
}