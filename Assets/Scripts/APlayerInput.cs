using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class APlayerInput : MonoBehaviour
{
    private Player _player;
    public Vector2 MovementInputValue { get; private set; }
    #region Input

    private PlayerInput _input;
    public bool IsMoveButtonPressed { get; private set; }
    public bool IsAttackButtonPressed { get; private set; }
    public bool IsJumpButtonPressed { get; private set; }
    public bool IsDashButtonPressed { get; private set; }

    #endregion

    private void Awake()
    {
        _input = new PlayerInput();
        _player = GetComponent<Player>();
        _input.CharacterControls.Move.performed += OnMove;
        _input.CharacterControls.Attack.started += OnAttack;
        _input.CharacterControls.Attack.canceled += OnAttack;
        _input.CharacterControls.Jump.started += OnJump;
        _input.CharacterControls.Jump.canceled += OnJump;
        _input.CharacterControls.Dash.started += OnDash;
        _input.CharacterControls.Dash.canceled += OnDash;
    }

    private void OnMove(InputAction.CallbackContext context)
    {

        MovementInputValue = context.ReadValue<Vector2>();
        _player.movement.x = MovementInputValue.x;
        IsMoveButtonPressed = _player.movement.x != 0;

    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        IsAttackButtonPressed = context.ReadValueAsButton();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        IsJumpButtonPressed = context.ReadValueAsButton();
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        IsDashButtonPressed = context.ReadValueAsButton();
    }

    private void OnEnable()
    {
        _input.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _input.CharacterControls.Disable();
    }
}