using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Scaner))]
public class Base : MonoBehaviour
{
    [SerializeField] private Scaner _scaner;
    [SerializeField] private List<UnitSpawnPoint> _unitSpawnPoints;
    [SerializeField] private UnitSpawner _spawner;

    private void Awake()
    {
        
    }

    public void SpawnUnits()
    {
        foreach (UnitSpawnPoint point in _unitSpawnPoints)
        {
            
        }
    }
}
