using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]

public class Unit : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Transform _target;

    private void Start()
    {
        _mover.StartMoving(_target.position);
    }

}