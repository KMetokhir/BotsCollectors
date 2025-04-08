using System;
using UnityEngine;

public class BaseCollisionHandler : MonoBehaviour
{
    public Action<IResourceHandler> ResourceHandlerCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IResourceHandler handler))
        {
            ResourceHandlerCollision?.Invoke(handler);
        }
    }
}