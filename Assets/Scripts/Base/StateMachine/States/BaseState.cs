using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    [SerializeField] private BaseState _targetState;

    public event Action<BaseState, BaseState> �onditionsToChangeState;

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

    protected void InvokeChangeStateEvent()
    {
        �onditionsToChangeState?.Invoke(this,_targetState);
    }
}