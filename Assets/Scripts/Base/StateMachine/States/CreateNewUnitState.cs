using System;
using UnityEngine;

public class CreateNewUnitState : BaseState
{
    [SerializeField] private Storage _storage;
    [SerializeField] private UnitGenerator _unitGenerator;
    [SerializeField] private uint _unitCost;

    [SerializeField] private Flag _flag;

    private void OnEnable()
    {
        _storage.ValueChanged += OnStorageValueChanged;
        _flag.Installed += OnFlagInstalledEvent;
    }

    private void OnDisable()
    {
        _storage.ValueChanged -= OnStorageValueChanged;
        _flag.Installed -= OnFlagInstalledEvent;
    }

    private void OnFlagInstalledEvent(Flag flag)
    {
        InvokeChangeStateEvent();
    }

    private void OnStorageValueChanged(int storageValue)
    {
        if (storageValue >= _unitCost)
        {
            if (_unitGenerator.TryGenerateUnit())
            {
                _storage.Reduce(_unitCost);
            }
        }
    }
}