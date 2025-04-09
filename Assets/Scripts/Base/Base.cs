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

    private List<Unit> _units;

    private void Awake()
    {
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
            ResourceData.Instance.AddResources(resources);
        }

        if (ResourceData.Instance.IsEmpty)
        {
            return;
        }

        if (TryGetAllAvalibleUnits(out IEnumerable<Unit> units))
        {
            foreach (Unit unit in units)
            {
                if (TrySetTargetForUnit(unit) == false)
                {
                    return;
                }
            }
        }
    }

    private void ProcessResourceHandlerCollision(ICollectableHandler handler)
    {
        Unit unit = handler as Unit;

        if (_units.Contains(unit))
        {
            if (handler.TryGetCollectable(out ICollectable collectable))
            {
                Resource resource = collectable as Resource;

                if (resource != null)
                {
                    _storage.AddResource();
                    ResourceData.Instance.Remove(resource);
                    resource.Destroy();
                }
            }
        }
    }

    private bool TrySetTargetForUnit(Unit unit)
    {
        bool targetSet = false;

        if (unit.IsAvalible)
        {
            if (ResourceData.Instance.TryGetUncollectedResource(out Resource resource))
            {
                unit.SetTarget(resource);
                targetSet = true;
            }
        }

        return targetSet;
    }

    private bool TryGetAllAvalibleUnits(out IEnumerable<Unit> avalibleUnits)
    {
        avalibleUnits = _units.Where(unit => unit.IsAvalible == true);

        bool unitsFound = avalibleUnits.Count() > 0;

        return unitsFound;
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
        if (ResourceData.Instance.IsEmpty)
        {
            Scan();
        }
    }
}