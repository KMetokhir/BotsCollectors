using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Base _base;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Player.Scan.performed += OnScan;
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Scan.performed -= OnScan;
        _playerInput.Disable();
    }

    private void OnScan(InputAction.CallbackContext context)
    {
        _base.Scan();
    }
}