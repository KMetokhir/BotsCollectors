using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Bag))]
public class Unit : MonoBehaviour, ICollectableHandler, UnitEvents
{
    [SerializeField] private Mover _mover;
    [SerializeField] private TargetHandler _targetHandler;
    [SerializeField] private Bag _bag;
    [SerializeField] private BaseSpawner _baseSpawner;

    public event Action<Unit> BecameAvailable;
    public event Action<Unit> ResourceCollected;
    public event Action<Unit> NewBaseBuild;

    public bool IsAvalible => _bag.IsEmpty && _targetHandler.IsAvalible;

    private void OnEnable()
    {
        _targetHandler.FlagFound += OnFlagFound;
        _targetHandler.CollectableFound += OnCollectableFound;
    }

    private void Start()
    {
        if (IsAvalible)
        {
            BecameAvailable?.Invoke(this);
        }
    }

    private void OnDisable()
    {
        _targetHandler.FlagFound -= OnFlagFound;
        _targetHandler.CollectableFound -= OnCollectableFound;
    }

    public void ResetTarget()
    {
        _mover.StopMoving();
        _targetHandler.ResetTarget();
    }

    public void SetTarget(IUnitTarget target)
    {
        if (_targetHandler.IsAvalible == false)
        {
            throw new Exception("Unit " + gameObject.ToString() + " isn't avalible");
        }
        else
        {
            _targetHandler.SetTarget(target);
            _mover.StartMoving(target.Position);
        }
    }

    public void MoveTo(Vector3 position)
    {
        _mover.StartMoving(position);
    }

    public bool TryGetCollectable(out ICollectable collectable)
    {
        if (_bag.TryGetCollectable(out collectable))
        {
            BecameAvailable?.Invoke(this);
        }

        bool isCollectableGotten = collectable != null;

        return isCollectableGotten;
    }

    private void OnCollectableFound(ICollectable collectable)
    {
        _mover.StopMoving();
        _bag.PutCollectable(collectable);
        ResourceCollected?.Invoke(this);
    }

    private void OnFlagFound(Flag flag)
    {
        Base newBase = _baseSpawner.Spawn(flag.Position);
        newBase.AddUnit(this);// ADD SPAWN POINT FOR UNIT IN BASE!!!!
        NewBaseBuild?.Invoke(this);
    }
}