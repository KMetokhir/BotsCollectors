using System;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    [SerializeField] private BaseState _startState;

    private BaseState _currentState;

    private void Start()
    {
        _currentState = _startState;
        _currentState.�onditionsToChangeState += OnConditionChangedState;
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

        _currentState.�onditionsToChangeState -= OnConditionChangedState;
        _currentState = nextState;
        _currentState.�onditionsToChangeState += OnConditionChangedState;

        if (_currentState)
        {
            _currentState.Enter();
        }
    }
}