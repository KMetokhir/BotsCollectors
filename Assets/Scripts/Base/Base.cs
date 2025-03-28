using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using System.Linq;

[RequireComponent(typeof(Scaner))]
public class Base : MonoBehaviour
{
    [SerializeField] private Scaner _scaner;
    [SerializeField] private List<UnitPoint> _unitSpawnPoints;
    [SerializeField] private UnitSpawner _spawner;    

    private List<Resource> _uncollectedResources;
    private List<Unit> _units;

    private void Awake()
    {
        _uncollectedResources = new List<Resource>();
        _units = new List<Unit>();

        SpawnUnits();

        if (_scaner.TryGetResources(transform.position, out List<Resource> resources))
        {
            _uncollectedResources = resources;
        }


        List<Resource> nonAvalibleresources = new List<Resource>();

        foreach (Resource resource in _uncollectedResources)
        {
            if (TryFindFirstAvalibleUnit(out Unit unit))
            {
                nonAvalibleresources.Add(resource);
                unit.SetTargetResource(resource);
            }
        }

        foreach (Resource resource in nonAvalibleresources)
        {
            _uncollectedResources.Remove(resource);
        }
    }

    private void OnEnable()
    {
        foreach (Unit unit in _units)
        {
            unit.BecameAvailable += OnUnitBecameAvailable;
            unit.ResourceCollected += OnUnitCollectedResource;
        }        
    }   

    private void OnDisable()
    {
        foreach (Unit unit in _units)
        {
            unit.BecameAvailable -= OnUnitBecameAvailable;
            unit.ResourceCollected -= OnUnitCollectedResource;
        }
    }

    private void SpawnUnits()
    {
        foreach (UnitPoint point in _unitSpawnPoints)
        {
            if (point.TryGetPosition(out Vector3 position))
            {
                Unit unit = _spawner.Spawn(position);
                point.SetUnit(unit);
                _units.Add(unit);
            }
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
        if (_uncollectedResources.Count > 0)
        {
            Resource resource = _uncollectedResources.First();
            unit.SetTargetResource(resource);
            _uncollectedResources.Remove(resource); // create method try get avalable resource
        }
    }
}