using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

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

        /*position = new Vector3(position.x, transform.position.y, position.z);
        Vector3 direction = (position - transform.position).normalized;

        _rigidbody.velocity = direction * _speed;*/
    }

    public void StopMoving()
    {
        _isMoving = false;        
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
        }
    }
}