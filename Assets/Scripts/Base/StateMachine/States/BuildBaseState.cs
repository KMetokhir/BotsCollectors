using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBaseState : BaseState
{
    [SerializeField] Flag _flag;
    [SerializeField] private Storage _storage;
    [SerializeField] private uint _newBaseCost;
    [SerializeField] private UnitsData _unitData;
    [SerializeField] private SpawnPointsData _spawnPointsData;

    private Unit _unitBuilder;

    private void OnEnable()
    {
        _storage.ValueChanged += OnStorageValueChanged;
        _flag.Uninstalled += OnFlagUninstalled;
        _flag.Installed += OnFlagInstalled;
    }  

    private void OnFlagUninstalled()
    {
        if(_unitBuilder != null)
        {
            Vector3 position = _spawnPointsData.GetUnitSpawnPosition(_unitBuilder);
            _unitBuilder.ResetTarget();
            _unitBuilder.MoveTo(position);
        }
    }

    private void OnDisable()
    {
        _storage.ValueChanged -= OnStorageValueChanged;
    }

    private void OnStorageValueChanged(int value)
    {
        TrySendUnitToFlag();
    }

    private bool TrySendUnitToFlag()
    {
        bool isSuccess = false;

        if (_storage.Value >= _newBaseCost)
        {
            if (_unitData.TryGetAvalibleUnit(out Unit unit))
            {
                _unitBuilder = unit;
                unit.SetTarget(_flag);
                isSuccess = true;
            }
        }

        return isSuccess;
    }

    private void OnFlagInstalled(Flag flag)
    {
        if (_unitBuilder != null)
        {
            _unitBuilder.ResetTarget();
            _unitBuilder.SetTarget(flag);
        }
    }
}