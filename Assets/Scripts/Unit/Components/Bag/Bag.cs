using System;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private BagpackPoint _bagPackpoint;

    private ICollectable _collectable;

    public bool IsEmpty => _collectable == null;

    public void PutCollectable(ICollectable collectable)
    {
        if (IsEmpty == false)
        {
            throw new Exception("Bag is full");
        }

        _collectable = collectable;
        collectable.Collect(transform, _bagPackpoint.transform.position);
    }

    public bool TryGetCollectable(out ICollectable collectable)
    {
        collectable = _collectable;

        if (IsEmpty == false)
        {
            _collectable = null;
        }

        bool isCollectableGotten = collectable != null;

        return isCollectableGotten;
    }
}