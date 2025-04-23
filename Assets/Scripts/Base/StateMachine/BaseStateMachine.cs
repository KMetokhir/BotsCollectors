using System;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    [SerializeField] private BaseState _startState;

    private BaseState _currentState;

    private void Start()
    {
        _currentState = _startState;
        _currentState.ÑonditionsToChangeState += OnConditionChangedState;
        _currentState.Enter();
    }

    private void OnConditionChangedState(BaseState nextState)
    {
        Transit(nextState);
    }

    private void Transit(BaseState nextState)
    {
        if (_currentState)
            _currentState.Exit();

        _currentState.ÑonditionsToChangeState -= OnConditionChangedState;
        _currentState = nextState;
        _currentState.ÑonditionsToChangeState += OnConditionChangedState;

        if (_currentState)
        {
            _currentState.Enter();
        }
    }
}