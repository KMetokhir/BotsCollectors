using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Collector))]
public class Unit : MonoBehaviour, ICollectableHandler
{
    [SerializeField] private Mover _mover;

    [SerializeField] private Collector _collector;

    public event Action<Unit> BecameAvailable;
    public event Action<Unit> ResourceCollected;

    public bool IsAvalible => _collector.IsAvalible;

    private void OnEnable()
    {
        _collector.BecameAvailable += OnBecameAvalible;
        _collector.ResourceCollected += OnResourceCollected;
    }

    private void OnDisable()
    {
        _collector.BecameAvailable -= OnBecameAvalible;
        _collector.ResourceCollected -= OnResourceCollected;
    }

    public void SetTarget(ICollectable target)
    {
        if (_collector.IsAvalible == false)
        {
            throw new Exception("Unit " + gameObject.ToString() + " isn't avalible");
        }
        else
        {
            _collector.SetTarget(target);
            _mover.StartMoving(target.Position);
        }
    }

    public void MoveTo(Vector3 position)
    {
        _mover.StartMoving(position);
    }

    public bool TryGetCollectable(out ICollectable collectable)
    {
        return _collector.TryGetCollectable(out collectable);
    }

    private void OnResourceCollected()
    {
        _mover.StopMoving();
        ResourceCollected?.Invoke(this);
    }

    private void OnBecameAvalible()
    {
        BecameAvailable?.Invoke(this);
    }
}