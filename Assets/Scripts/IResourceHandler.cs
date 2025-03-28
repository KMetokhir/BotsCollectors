using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceHandler
{
    public bool TryGetResource(out Resource resource);
}
