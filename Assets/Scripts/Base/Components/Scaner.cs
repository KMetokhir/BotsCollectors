using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private int _maxCollidersPerScan;

    private Collider[] _hitColliders;
    private List<Resource> _resources;

    private void OnValidate()
    {
        _scanRadius = Mathf.Abs(_scanRadius);
        _maxCollidersPerScan = Mathf.Abs(_maxCollidersPerScan);
    }

    private void Awake()
    {
        _hitColliders = new Collider[_maxCollidersPerScan];
        _resources = new List<Resource>(_maxCollidersPerScan);
    }

    public bool TryGetResources(Vector3 position, out List<Resource> resources)
    {
        _resources.Clear();

        int collidersCount = Physics.OverlapSphereNonAlloc(position, _scanRadius, _hitColliders);

        for (int i = 0; i < collidersCount; i++)
        {
            if (_hitColliders[i].transform.TryGetComponent(out Resource resource))
            {
                _resources.Add(resource);
            }
        }

        resources = _resources;

        return resources.Count > 0;
    }
}