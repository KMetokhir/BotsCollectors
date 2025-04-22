using UnityEngine;

public class CreateNewUnitState : BaseState
{
    [SerializeField] private Storage _storage;
    [SerializeField] private UnitGenerator _unitGenerator;
    [SerializeField] private UnitsData _unitsData;
    [SerializeField] private uint _unitCost;
    [SerializeField] private uint _unitBuildBaseLimit;

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
        if (_unitsData.Count >= _unitBuildBaseLimit)
        {
            InvokeChangeStateEvent();
        }
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