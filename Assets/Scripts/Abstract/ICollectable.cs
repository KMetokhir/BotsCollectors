using UnityEngine;

public interface ICollectable
{
    public Vector3 Position { get; }
    public void Collect(Transform ownerTransform, Vector3 newPosition);
}