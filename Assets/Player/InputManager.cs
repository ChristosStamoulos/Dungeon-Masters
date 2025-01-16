using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls _playerControls;

    PlayerControls Controls
    {
        get
        {
            if (_playerControls == null)
            {
                _playerControls = new PlayerControls();
                _playerControls.PlayerControlMovment.Movment.performed += i => movementInput = i.ReadValue<Vector2>();
            }

            return _playerControls;
        }
    }

    public  Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;

    public void Start()
    {
        
    }

    private void OnEnable()
    {
        Enable(true);
    }

    private void OnDisable()
    {
        Enable(false);
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    public void HandleInputs()
    {
        HandleMovementInput();
    }

    public void Enable(bool enable)
    {
        if (enable)
        {
            Controls.Enable();
        }
        
        else
        {
            Controls.Disable();
        }
    }
}
