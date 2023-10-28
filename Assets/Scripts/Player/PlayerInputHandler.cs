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
    private PlayerCamera _playerCamera;
    private PlayerAttack _playerAttack;
    public PlayerMovement _playerMovmement;
    private PlayerInteract _playerInteract;
    
    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _playerMovmement = GetComponent<PlayerMovement>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<PlayerCamera>();
        _playerInteract = GetComponent<PlayerInteract>();
    }

    public void DisableInputHandling()
    {
        _playerInputAction.Disable();
    }

    public void EnableInputHandling()
    {
        _playerInputAction.Enable();
    }

    /*
    * When the player controller is enabled, activate its functionality
    */
    private void OnEnable()
    {
        _playerInputAction.Enable();

        _playerInputAction.Player.CamSwap.performed += OnCamSwap;

        // Assign functions as event handlers for input values
        _playerInputAction.Player.Move.performed += OnMovement;
        _playerInputAction.Player.Move.canceled += OnMovementStopped;

        _playerInputAction.Player.Sprint.started += OnSprint;
        //_playerInputAction.Player.Sprint.canceled += OnSprint;

        _playerInputAction.Player.Jump.started += OnJump;
        _playerInputAction.Player.Jump.canceled += OnJump;

        _playerInputAction.Player.Dash.started += OnDash;
        //_playerInputAction.Player.Jump.canceled += OnDash;

        _playerInputAction.Player.EquipAttack1.started += OnEquipAttack1;
        _playerInputAction.Player.EquipAttack2.started += OnEquipAttack2;
        _playerInputAction.Player.EquipAttack3.started += OnEquipAttack3;
        _playerInputAction.Player.EquipAttack4.started += OnEquipAttack4;

        _playerInputAction.Player.Attack.started += OnAttack;
        _playerInputAction.Player.CancelAttack.started += OnCancelAttack;

        _playerInputAction.Player.Interact.started += OnInteract;
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

        _playerInputAction.Player.EquipAttack1.started -= OnEquipAttack1;
        _playerInputAction.Player.EquipAttack2.started -= OnEquipAttack2;
        _playerInputAction.Player.EquipAttack3.started -= OnEquipAttack3;
        _playerInputAction.Player.EquipAttack4.started -= OnEquipAttack4;

        _playerInputAction.Player.Attack.started -= OnAttack;
        _playerInputAction.Player.CancelAttack.started -= OnCancelAttack;

        _playerInputAction.Player.Interact.started -= OnInteract;
    }

    private void OnCamSwap(InputAction.CallbackContext value)
    {
        _playerCamera.SwitchCameraStyle();
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

    private void OnEquipAttack1(InputAction.CallbackContext context)
    {
        _playerAttack.OnEquipAttack(AttackSlot.Slot1);
    }

    private void OnEquipAttack2(InputAction.CallbackContext context)
    {
        _playerAttack.OnEquipAttack(AttackSlot.Slot2);
    }

    private void OnEquipAttack3(InputAction.CallbackContext context)
    {
        _playerAttack.OnEquipAttack(AttackSlot.Slot3);
    }

    private void OnEquipAttack4(InputAction.CallbackContext context)
    {
        _playerAttack.OnEquipAttack(AttackSlot.Slot4);
    }

    private void OnAttack(InputAction.CallbackContext value)
    {
        _playerAttack.OnAttack();
    }

    private void OnCancelAttack(InputAction.CallbackContext context)
    {
        _playerAttack.OnCancelAttack();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        _playerInteract.OnInteractPressed();
    }
}
