using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Mover))]

public class Unit : MonoBehaviour, IResourceCollector, IResourceHandler
{
    [SerializeField] private Mover _mover;
    [SerializeField] private BagpackPoint _bagPackpoint;

    private Resource _targetResource;
    private bool _isResourceCollected;

    public event Action<Unit> BecameAvailable;
    public event Action<Unit> ResourceCollected;

    public bool IsAvalible => _targetResource == null;

    private void Awake()
    {
        _isResourceCollected = false;
    }
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

    public bool TryCollectResource(Resource resource)
    {
        _isResourceCollected = false;

        if (_targetResource == resource)
        {
            _mover.StopMoving();

            ResourceCollected?.Invoke(this);            
            resource.transform.parent = transform;
            resource.transform.position = _bagPackpoint.transform.position;
            _isResourceCollected = true;            
        }

        return _isResourceCollected;
    }

    public void MoveTo(Vector3 position)
    {
        _mover.StartMoving(position);
    }

    public bool TryGetResource(out Resource resource)
    {
        bool isResourceGeted = false;

        if (_isResourceCollected)
        {
            resource = _targetResource;
            isResourceGeted = true;
            _isResourceCollected = false;
            _targetResource = null;

            BecameAvailable?.Invoke(this);
        }
        else
        {
            resource = null;
        }

        return isResourceGeted;
    }   
}