using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Resource : MonoBehaviour
{
    private CapsuleCollider _capsuleCollider;

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IResourceCollector collector))
        {
            bool isCollected = collector.TryCollectResource(this);

            if (isCollected)
            {
                _capsuleCollider.enabled = false;
            }
        }
    }
}