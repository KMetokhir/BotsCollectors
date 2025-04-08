using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;

    private void OnValidate()
    {
        _scanRadius = Mathf.Abs(_scanRadius);
    }

    public bool TryGetResources(Vector3 position, out List<Resource> resources)
    {
        resources = new List<Resource>();

        Collider[] hitColliders = Physics.OverlapSphere(position, _scanRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.transform.TryGetComponent(out Resource resource))
            {
                resources.Add(resource);
            }
        }

        bool isResurcedExist = resources.Count > 0;

        return isResurcedExist;
    }
}