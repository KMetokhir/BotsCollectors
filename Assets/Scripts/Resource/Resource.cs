using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(CapsuleCollider))]
public class Resource : MonoBehaviour, ICollectable
{
    private CapsuleCollider _capsuleCollider;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Collect(Transform ownerTransform, Vector3 newPosition)
    {
        _capsuleCollider.enabled = false;

        transform.parent = ownerTransform;
        transform.position = newPosition;
    }
}