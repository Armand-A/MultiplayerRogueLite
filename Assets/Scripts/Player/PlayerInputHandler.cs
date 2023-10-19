using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputAction _playerInputAction;
    private PlayerAttack _playerAttack;
    public Vector2 MoveVector = Vector2.zero;
    public Vector2 LookVector = Vector2.zero;
    public float SprintBool = 0.0f;
    public float JumpBool = 0.0f;

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _playerAttack = GetComponent<PlayerAttack>();
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

        _playerInputAction.Player.EquipAttack1.started += OnEquipAttack1;
        _playerInputAction.Player.EquipAttack2.started += OnEquipAttack2;
        _playerInputAction.Player.EquipAttack3.started += OnEquipAttack3;
        _playerInputAction.Player.EquipAttack4.started += OnEquipAttack4;

        _playerInputAction.Player.Attack.started += OnAttack;
        _playerInputAction.Player.CancelAttack.started += OnCancelAttack;
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

        _playerInputAction.Player.EquipAttack1.started -= OnEquipAttack1;
        _playerInputAction.Player.EquipAttack2.started -= OnEquipAttack2;
        _playerInputAction.Player.EquipAttack3.started -= OnEquipAttack3;
        _playerInputAction.Player.EquipAttack4.started -= OnEquipAttack4;
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
}
