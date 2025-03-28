using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
public class Resource : MonoBehaviour
{
    private CapsuleCollider _capsuleCollider;
    //private bool _isCollected;

    private void Awake()
    {
        //_isCollected = false;
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        /* if (_isCollected)
         {
             return;
         }*/

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
