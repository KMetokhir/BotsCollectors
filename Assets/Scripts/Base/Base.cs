using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : UnitGenerator, IFlagHolder
{
    [SerializeField] private Scaner _scaner;
    [SerializeField] private SpawnPointsData _spawnPointsData;
    [SerializeField] private UnitSpawner _spawner;
    [SerializeField] private BaseCollisionHandler _collisionHandler;
    [SerializeField] private Storage _storage;
    [SerializeField] private UnitsData _unitData;
    [SerializeField] private Flag _flag;

    private void OnEnable()
    {
        _collisionHandler.ResourceHandlerCollision += ProcessResourceHandlerCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.ResourceHandlerCollision -= ProcessResourceHandlerCollision;

        foreach (UnitEvents events in _unitData.UnitsEvents)
        {
            events.BecameAvailable -= OnUnitBecameAvailable;
            events.ResourceCollected -= OnUnitCollectedResource;
        }
    }

    public override bool TryGenerateUnit()
    {
        bool isSpawned = false;

        if (_spawnPointsData.TryGetEmptyPoint(out UnitPoint point))
        {
            if (point.TryGetPosition(out Vector3 position))
            {
                Unit unit = _spawner.Spawn(position);
                point.SetUnit(unit);
                _unitData.Add(unit);

                isSpawned = true;

                unit.BecameAvailable += OnUnitBecameAvailable;
                unit.ResourceCollected += OnUnitCollectedResource;
                unit.NewBaseBuild += OnNewBaseBuild;
            }
        }

        return isSpawned;
    }

    public void AddUnit(Unit unit)
    {
        _unitData.Add(unit);
    }

    public Flag GetFlag()
    {
        if (_flag == null)
        {
            throw new NullReferenceException(" Flag = null ");
        }

        return _flag;
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

        if (_unitData.TryGetAllAvalibleUnits(out IEnumerable<Unit> units))
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

    private void OnNewBaseBuild(Unit unit)
    {
        _unitData.RemoveUnit(unit);
        _spawnPointsData.ClearPoint(unit);

        unit.BecameAvailable -= OnUnitBecameAvailable;
        unit.ResourceCollected -= OnUnitCollectedResource;
        unit.NewBaseBuild -= OnNewBaseBuild;
    }

    private void ProcessResourceHandlerCollision(ICollectableHandler handler)
    {
        Unit unit = handler as Unit;

        if (_unitData.Contains(unit))
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

    private void OnUnitCollectedResource(Unit unit)
    {
        Vector3 position = _spawnPointsData.GetUnitSpawnPosition(unit);
        unit.MoveTo(position);
    }

    private void OnUnitBecameAvailable(Unit unit)
    {
        if (ResourceData.Instance.IsEmpty)
        {
            Scan();
        }
        // send to BASE!
    }
}