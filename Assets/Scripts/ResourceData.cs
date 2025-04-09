using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceData : MonoBehaviour
{
    private List<Resource> _uncollectedResources;
    private List<Resource> _resourcesInProcess;

    public static ResourceData Instance { get; private set; }
    public bool IsEmpty => _uncollectedResources.Count == 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);

            return;
        }

        Instance = this;

        _uncollectedResources = new List<Resource>();
        _resourcesInProcess = new List<Resource>();
    }

    public void Remove(Resource resource)
    {
        if (_resourcesInProcess.Contains(resource))
        {
            _resourcesInProcess.Remove(resource);
        }
        else
        {
            throw new System.Exception("_resourcesInProcess List does not contain resource " + resource.gameObject.ToString());
        }
    }

    public void AddResources(List<Resource> resources)
    {
        resources = resources.Except(_resourcesInProcess).ToList();
        _uncollectedResources.AddRange(resources);
        _uncollectedResources = _uncollectedResources.Distinct().ToList();
    }

    public bool TryGetUncollectedResource(out Resource resource)
    {
        bool isResourceExists = false;
        resource = null;

        if (_uncollectedResources.Count == 0 || _uncollectedResources == null)
        {
            isResourceExists = false;
        }
        else
        {
            isResourceExists = true;

            resource = _uncollectedResources.First();
            _resourcesInProcess.Add(resource);
            _uncollectedResources.Remove(resource);
        }

        return isResourceExists;
    }
}