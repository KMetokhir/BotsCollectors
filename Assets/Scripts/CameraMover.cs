using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _followSpeed;

    [SerializeField] private Transform _folower;
    private Camera _camera;
    private float _screenWidth = Screen.width;
    private float _screenHeight = Screen.height;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (TryGetDirection(_screenWidth, _screenHeight, out Vector3 direction))
        {
            Follow(direction, _folower);
        }
    }

    private bool TryGetDirection(float xUpperBound, float yUpperBound, out Vector3 direction)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        direction = Vector3.zero;
        float xYLowerBound = 0;

        if (mousePosition.x <= xYLowerBound)
        {
            direction = Vector3.left;
        }
        else if (mousePosition.x >= xUpperBound)
        {
            direction = Vector3.right;
        }

        if (mousePosition.y <= xYLowerBound)
        {
            direction = Vector3.back;
        }
        else if (mousePosition.y >= yUpperBound)
        {
            direction = Vector3.forward;
        }

        bool isDirectionExist = direction != Vector3.zero;

        return isDirectionExist;
    }

    private void Follow(Vector3 direction, Transform folower)
    {
        folower.position += direction * _followSpeed * Time.deltaTime;
    }
}