using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Mover))]

public class Unit : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private BagpackPoint _bagPackpoint;

    private Resource _targetResource;

    public event Action<Unit> BecameAvailable;
    public event Action<Resource> TookResource;

    public bool IsAvalible => _targetResource == null;

    private void Start()
    {
        if (_targetResource == null)
        {
            BecameAvailable?.Invoke(this);
        }
    }

    public void SetTargetResource(Resource resource)
    {
        if (IsAvalible == false)
        {
            throw new Exception("Unit " + gameObject.ToString() + " isn't avalible");
        }
        else
        {
            _targetResource = resource;
            _mover.StartMoving(_targetResource.transform.position);
        }
    }

    public bool TryTakeResource(Resource resource)
    {
        bool isResourceRequired = false;

        if (_targetResource == resource)
        {
            TookResource?.Invoke(resource);
            resource.transform.parent = transform;
            resource.transform.position = _bagPackpoint.transform.position;
            isResourceRequired = true;

            _mover.StopMoving();
        }

        return isResourceRequired;
    }

    public bool TryGetResource(out Resource resource)
    {

    }

    public void MoveTo(Vector3 position)
    {
        _mover.StartMoving(position);
    }
}