using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private FlagInstaller _flagInstaller;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Player.Scan.performed += OnScan;
        _playerInput.Player.LeftMouseButtonClick.performed += OnLeftButtonMouseClicked;
        _playerInput.Enable();
    }

    private void OnLeftButtonMouseClicked(InputAction.CallbackContext context)
    {
        Vector2 tapPoint = context.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(tapPoint);

        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            if (hitData.collider != null)
            {
                _flagInstaller.ProcessMouseTap(hitData);
            }
        }
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