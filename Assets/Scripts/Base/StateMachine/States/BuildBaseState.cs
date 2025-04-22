using System;
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

        SendUnitToFlag();
    }

    private void OnDisable()
    {
        _storage.ValueChanged -= OnStorageValueChanged;
        _flag.Uninstalled -= OnFlagUninstalled;
        _flag.Installed -= OnFlagInstalled;
    }

    private void OnFlagUninstalled()
    {
        if (_unitBuilder != null)
        {
            Vector3 position = _spawnPointsData.GetUnitSpawnPosition(_unitBuilder);
            _unitBuilder.ResetTarget();
            _unitBuilder.MoveTo(position);
        }
    }

    private void OnStorageValueChanged(int value)
    {
        if (_unitBuilder == null)
        {
            SendUnitToFlag();
        }
    }

    private void SendUnitToFlag()
    {
        if (_unitBuilder != null)
        {
            throw new Exception("unitBuilder allready send");
        }

        if (_storage.Value >= _newBaseCost)
        {
            if (_unitData.TryGetAvalibleUnit(out Unit unit))
            {
                _unitBuilder = unit;
                unit.SetTarget(_flag);
                unit.NewBaseBuild += OnNewBaseBuild;

            }
        }
    }

    private void OnNewBaseBuild(Unit unit, Base newBase)
    {
        if (_unitBuilder != unit)
        {
            throw new Exception("unitBuilder subscribe error");
        }

        _storage.Reduce(_newBaseCost);

        unit.NewBaseBuild -= OnNewBaseBuild;
        _unitBuilder = null;

        _flag.Uninstalled -= OnFlagUninstalled;
        _flag.Uninstall();

        InvokeChangeStateEvent();
    }

    private void OnFlagInstalled(Flag flag)
    {
        if (_unitBuilder != null)
        {
            _unitBuilder.ResetTarget();
            _unitBuilder.SetTarget(flag);
        }
        else
        {
            SendUnitToFlag();
        }
    }
}