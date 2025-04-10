using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    [SerializeField] private BaseState _targetState;
    [SerializeField] private BaseStateMachine _stateMachine;

    private void Awake()
    {
        enabled = false;
    }

    public virtual void Enter()
    {
        if (enabled == false)
        {
            enabled = true;
        }
    }

    public virtual void Exit()
    {
        enabled = false;
    }

    protected void TransitToTarGetState()
    {
        _stateMachine.Transit(this, _targetState);
    }
}