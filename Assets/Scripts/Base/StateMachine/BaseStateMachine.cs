using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    [SerializeField] private BaseState _startState;

    private BaseState _currentState;

    private void Start()
    {
        _currentState = _startState;
        _currentState.Enter();
    }

    public void Transit(BaseState stateInvoker, BaseState nextState)
    {
        if (_currentState != stateInvoker && _currentState != null)
        {
            throw new Exception($"State Invoker {stateInvoker.name} is not current active state  {_currentState.name}");
        }

        if (_currentState)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState)
        {
            _currentState.Enter();
        }
    }
}