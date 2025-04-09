using System;
using UnityEngine;

public class BaseCollisionHandler : MonoBehaviour
{
    public Action<ICollectableHandler> ResourceHandlerCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectableHandler handler))
        {
            ResourceHandlerCollision?.Invoke(handler);
        }
    }
}