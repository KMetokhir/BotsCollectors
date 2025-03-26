using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;

    private void OnValidate()
    {
        _explosionRadius = Mathf.Abs(_explosionRadius);
    }

    private List<Resource> GetResources(Vector3 position, float explosionRadius)
    {
        List<Resource> resources = new List<Resource>();

        Collider[] hitColliders = Physics.OverlapSphere(position, explosionRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.transform.TryGetComponent(out Resource resource))
            {
                resources.Add(resource);
            }
        }

        return resources;
    }
}
