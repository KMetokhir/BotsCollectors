using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private FlagInstaller _flagInstaller;

    private HashSet<Base> _bases;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _bases = new HashSet<Base>();
        _bases.Add(_base);
    }

    private void OnEnable()
    {
        _playerInput.Player.Scan.performed += OnScan;
        _playerInput.Player.LeftMouseButtonClick.performed += OnLeftButtonMouseClicked;
        _playerInput.Enable();

        _base.NewBaseBuild += OnNewBaseBuild;
    }

    private void OnDisable()
    {
        foreach (Base @base in _bases)
        {
            @base.NewBaseBuild -= OnNewBaseBuild;
        }

        _playerInput.Player.Scan.performed -= OnScan;
        _playerInput.Disable();
    }

    private void OnNewBaseBuild(Base newBase)
    {
        _bases.Add(newBase);

        newBase.NewBaseBuild += OnNewBaseBuild;
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

    private void OnScan(InputAction.CallbackContext context)
    {
        foreach (Base @base in _bases)
        {
            @base.Scan();
        }
    }
}