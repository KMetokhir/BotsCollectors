using System;
using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    private IUnitTarget _target;

    public event Action<Flag> FlagFound;
    public event Action<ICollectable> CollectableFound;

    public bool IsAvalible => _target == null;

    public void SetTarget(IUnitTarget target)
    {
        if (IsAvalible == false)
        {
            throw new Exception("Unit " + gameObject.ToString() + " isn't avalible");
        }
        else
        {
            _target = target;
        }
    }

    public void ResetTarget()
    {
        _target = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsAvalible)
        {
            return;
        }

        if (other.TryGetComponent(out IUnitTarget unitTarget))
        {
            if (unitTarget == _target)
            {
                if (unitTarget is ICollectable)
                {
                    CollectableFound?.Invoke(unitTarget as ICollectable);
                }
                else if (unitTarget is Flag)
                {
                    FlagFound?.Invoke(unitTarget as Flag);
                }

                _target = null;
            }
        }
    }
}