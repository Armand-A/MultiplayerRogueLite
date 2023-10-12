using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputAction _playerInputAction;
    public Vector2 MoveVector = Vector2.zero;
    public Vector2 LookVector = Vector2.zero;
    public float SprintBool = 0.0f;
    public float JumpBool = 0.0f;

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
    }

    /*
    * When the player controller is enabled, activate its functionality
    */
    private void OnEnable()
    {
        _playerInputAction.Enable();

        // Assign functions as event handlers for input values
        _playerInputAction.Player.Move.performed += OnMovement;
        _playerInputAction.Player.Move.canceled += OnMovementStopped;
        _playerInputAction.Player.Look.performed += OnLook;
        _playerInputAction.Player.Look.canceled += OnLookStopped;

        _playerInputAction.Player.Sprint.started += OnSprint;
        _playerInputAction.Player.Sprint.canceled += OnSprint;

        _playerInputAction.Player.Jump.started += OnJump;
        _playerInputAction.Player.Jump.canceled += OnJump;

        
        
    }

    /*
    * When the player controller is disabled, deactivate its functionality
    */
    private void OnDisable()
    {
        _playerInputAction.Disable();
        // Remove the event handlers
        _playerInputAction.Player.Move.performed -= OnMovement;
        _playerInputAction.Player.Move.canceled -= OnMovementStopped;
        _playerInputAction.Player.Look.performed -= OnLook;
        _playerInputAction.Player.Look.canceled -= OnLookStopped;

        _playerInputAction.Player.Sprint.started -= OnSprint;
        _playerInputAction.Player.Sprint.canceled -= OnSprint;

        _playerInputAction.Player.Jump.started -= OnJump;
        _playerInputAction.Player.Jump.canceled -= OnJump;

    }

    private void OnMovement(InputAction.CallbackContext value)
    {
        MoveVector = value.ReadValue<Vector2>();
    }
    private void OnLook(InputAction.CallbackContext value)
    {
        LookVector = value.ReadValue<Vector2>();
    }
    private void OnSprint(InputAction.CallbackContext value)
    {
        SprintBool = value.ReadValue<float>();
    }
    private void OnJump(InputAction.CallbackContext value)
    {
        JumpBool = value.ReadValue<float>();
    }

    /*
    * When no movement controls are detected, stop movement
    */
    private void OnMovementStopped(InputAction.CallbackContext value)
    {
        MoveVector = Vector2.zero;
    }
    private void OnLookStopped(InputAction.CallbackContext value)
    {
        LookVector = Vector2.zero;
    }
}
