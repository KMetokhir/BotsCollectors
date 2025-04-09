using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private BagpackPoint _bagPackpoint;

    private ICollectable _target;
    private bool _isTargetCollected;

    public bool IsAvalible => _target == null;

    public event Action BecameAvailable;
    public event Action ResourceCollected;

    private void Awake()
    {
        _isTargetCollected = false;
    }

    private void Start()
    {
        if (IsAvalible)
        {
            BecameAvailable?.Invoke();
        }
    }
    public void SetTarget(ICollectable target)
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

    public bool TryGetCollectable(out ICollectable collectable)
    {
        bool isResourceGeted = false;

        if (_isTargetCollected)
        {
            collectable = _target;
            isResourceGeted = true;
            _isTargetCollected = false;
            _target = null;

            BecameAvailable?.Invoke();
        }
        else
        {
            collectable = null;
        }

        return isResourceGeted;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isTargetCollected)
        {
            return;
        }

        if (other.TryGetComponent(out ICollectable collectable))
        {
            if (collectable == _target)
            {
                _isTargetCollected = true;

                collectable.Collect(transform, _bagPackpoint.transform.position);

                ResourceCollected?.Invoke();
            }
        }
    }
}