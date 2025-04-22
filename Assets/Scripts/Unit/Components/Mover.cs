using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _distanceOffset;

    private bool _isMoving = false;
    private Vector3 _target;

    private void OnValidate()
    {
        _speed = Mathf.Abs(_speed);
    }

    public void StartMoving(Vector3 position)
    {
        _isMoving = true;
        _target = position;
    }

    public void StopMoving()
    {
        _isMoving = false;
    }

    private float GetSqrDistance(Vector3 ownerPosition, Vector3 targetPosition)
    {
        targetPosition = new Vector3(targetPosition.x, ownerPosition.y, targetPosition.z);

        return (targetPosition - ownerPosition).sqrMagnitude;
    }

    private void RotateTo(Vector3 target)
    {
        target = new Vector3(target.x, transform.position.y, target.z);
        Vector3 relativePos = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void Move()
    {
        Vector3 velocity = new Vector3(transform.forward.x * _speed, _rigidbody.velocity.y, transform.forward.z * _speed);
        _rigidbody.velocity = velocity;
    }

    private void Update()
    {
        if (_isMoving)
        {
            Move();
            RotateTo(_target);

            float sqrDistance = GetSqrDistance(transform.position, _target);

            if (sqrDistance <= _distanceOffset)
            {
                StopMoving();
            }
        }
    }
}