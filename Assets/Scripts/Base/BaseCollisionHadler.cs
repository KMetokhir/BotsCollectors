using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollisionHadler : MonoBehaviour
{
    [SerializeField] private Storage _storage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IResourceHandler handler))
        {
            if (handler.TryGetResource(out Resource resource))
            {
                _storage.AddResource();
                resource.gameObject.SetActive(false);
            }
        }
    }
}
