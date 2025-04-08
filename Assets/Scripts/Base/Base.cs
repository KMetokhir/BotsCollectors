using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Base : MonoBehaviour
{
    [SerializeField] private Scaner _scaner;
    [SerializeField] private List<UnitPoint> _unitSpawnPoints;
    [SerializeField] private UnitSpawner _spawner;
    [SerializeField] private BaseCollisionHandler _collisionHandler;
    [SerializeField] private Storage _storage;

    private List<Resource> _uncollectedResources;
    private List<Resource> _resourcesInProcess;
    private List<Unit> _units;

    private void Awake()
    {
        _uncollectedResources = new List<Resource>();
        _resourcesInProcess = new List<Resource>();
        _units = new List<Unit>();
    }

    private void OnEnable()
    {
        _collisionHandler.ResourceHandlerCollision += ProcessResourceHandlerCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.ResourceHandlerCollision -= ProcessResourceHandlerCollision;

        foreach (Unit unit in _units)
        {
            unit.BecameAvailable -= OnUnitBecameAvailable;
            unit.ResourceCollected -= OnUnitCollectedResource;
        }
    }

    public void SpawnUnits()
    {
        foreach (UnitPoint point in _unitSpawnPoints)
        {
            if (point.TryGetPosition(out Vector3 position))
            {
                Unit unit = _spawner.Spawn(position);
                point.SetUnit(unit);
                _units.Add(unit);

                unit.BecameAvailable += OnUnitBecameAvailable;
                unit.ResourceCollected += OnUnitCollectedResource;
            }
        }
    }

    public void Scan()
    {
        if (_scaner.TryGetResources(transform.position, out List<Resource> resources))
        {
            resources = resources.Except(_resourcesInProcess).ToList();
            _uncollectedResources.AddRange(resources);
            _uncollectedResources = _uncollectedResources.Distinct().ToList();
        }

        SetTargetsForUnits();
    }

    private void ProcessResourceHandlerCollision(IResourceHandler handler)
    {
        Unit unit = handler as Unit;

        if (_units.Contains(unit))
        {
            if (handler.TryGetResource(out Resource resource))
            {
                _storage.AddResource();
                _resourcesInProcess.Remove(resource);
                resource.Destroy();
            }
        }
    }

    private void SetTargetsForUnits()
    {
        List<Resource> nonAvalibleresources = new List<Resource>();

        foreach (Resource resource in _uncollectedResources)
        {
            if (TryFindFirstAvalibleUnit(out Unit unit))
            {
                nonAvalibleresources.Add(resource);
                _resourcesInProcess.Add(resource);
                unit.SetTargetResource(resource);
            }
            else
            {
                break;
            }
        }

        foreach (Resource resource in nonAvalibleresources)
        {
            _uncollectedResources.Remove(resource);
        }
    }

    private bool TryFindFirstAvalibleUnit(out Unit unit)
    {
        unit = _units.FirstOrDefault(n => n.IsAvalible == true);

        bool isUnitFound = unit != null;

        return isUnitFound;
    }

    private void OnUnitCollectedResource(Unit unit)
    {
        foreach (UnitPoint point in _unitSpawnPoints)
        {
            if (point.TryGetPosition(unit, out Vector3 position))
            {
                unit.MoveTo(position);

                return;
            }
        }

        throw new Exception("Avalable position not found");
    }

    private void OnUnitBecameAvailable(Unit unit)
    {
        if (_uncollectedResources.Count == 0)
        {
            Scan();
        }

        if (_uncollectedResources.Count > 0)
        {
            SetTargetsForUnits();
        }
    }
}