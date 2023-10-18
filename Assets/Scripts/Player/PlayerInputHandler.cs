using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Reference
///     https://youtu.be/UyUogO2DvwY?list=PLO8cZuZwLyxNpkKDdu7I5K6iIpWLh9tbt
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputAction _playerInputAction;
    private PlayerAttack[] _playerAttacks;
    public PlayerMovement _playerMovmement;
    
    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _playerMovmement = GetComponent<PlayerMovement>();
        _playerAttacks = GetComponents<PlayerAttack>();
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

        _playerInputAction.Player.Sprint.started += OnSprint;
        //_playerInputAction.Player.Sprint.canceled += OnSprint;

        _playerInputAction.Player.Jump.started += OnJump;
        _playerInputAction.Player.Jump.canceled += OnJump;

        _playerInputAction.Player.Dash.started += OnDash;
        //_playerInputAction.Player.Jump.canceled += OnDash;

        _playerInputAction.Player.Attack1.started += OnAttack1;
        _playerInputAction.Player.Attack2.started += OnAttack2;
        _playerInputAction.Player.Attack3.started += OnAttack3;
        _playerInputAction.Player.Attack4.started += OnAttack4;

        _playerInputAction.Player.Attack1.canceled += OnAttack1Canceled;
        _playerInputAction.Player.Attack2.canceled += OnAttack2Canceled;
        _playerInputAction.Player.Attack3.canceled += OnAttack3Canceled;
        _playerInputAction.Player.Attack4.canceled += OnAttack4Canceled;
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

        _playerInputAction.Player.Sprint.started -= OnSprint;
        //_playerInputAction.Player.Sprint.canceled -= OnSprint;

        _playerInputAction.Player.Jump.started -= OnJump;
        _playerInputAction.Player.Jump.canceled -= OnJump;

        _playerInputAction.Player.Dash.started -= OnDash;
        //_playerInputAction.Player.Jump.canceled -= OnDash;

        _playerInputAction.Player.Attack1.started -= OnAttack1;
        _playerInputAction.Player.Attack2.started -= OnAttack2;
        _playerInputAction.Player.Attack3.started -= OnAttack3;
        _playerInputAction.Player.Attack4.started -= OnAttack4;

        _playerInputAction.Player.Attack1.canceled -= OnAttack1Canceled;
        _playerInputAction.Player.Attack2.canceled -= OnAttack2Canceled;
        _playerInputAction.Player.Attack3.canceled -= OnAttack3Canceled;
        _playerInputAction.Player.Attack4.canceled -= OnAttack4Canceled;
    }

    private void OnMovement(InputAction.CallbackContext value)
    {
        _playerMovmement.MoveVector = value.ReadValue<Vector2>();
    }
    private void OnSprint(InputAction.CallbackContext value)
    {
        _playerMovmement.SprintBool = value.ReadValue<float>();
    }
    private void OnJump(InputAction.CallbackContext value)
    {
        _playerMovmement.JumpBool = value.ReadValue<float>();
    }
    private void OnDash(InputAction.CallbackContext value)
    {
        _playerMovmement.DashBool = value.ReadValue<float>();
    }

    /*
    * When no movement controls are detected, stop movement
    */
    private void OnMovementStopped(InputAction.CallbackContext value)
    {
        _playerMovmement.MoveVector = Vector2.zero;
    }

    private void OnAttack1(InputAction.CallbackContext context)
    {
        _playerAttacks[0].OnAttackStarted();
    }

    private void OnAttack2(InputAction.CallbackContext context)
    {
        _playerAttacks[1].OnAttackStarted();
    }

    private void OnAttack3(InputAction.CallbackContext context)
    {
        _playerAttacks[2].OnAttackStarted();
    }

    private void OnAttack4(InputAction.CallbackContext context)
    {
        _playerAttacks[3].OnAttackStarted();
    }

    private void OnAttack1Canceled(InputAction.CallbackContext context)
    {
        _playerAttacks[0].OnAttackCanceled();
    }

    private void OnAttack2Canceled(InputAction.CallbackContext context)
    {
        _playerAttacks[1].OnAttackCanceled();
    }

    private void OnAttack3Canceled(InputAction.CallbackContext context)
    {
        _playerAttacks[2].OnAttackCanceled();
    }

    private void OnAttack4Canceled(InputAction.CallbackContext context)
    {
        _playerAttacks[3].OnAttackCanceled();
    }
}
