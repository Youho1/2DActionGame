using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
        stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.isAir = false;
        _player.SetVelocity(0.0f, 0.0f);
        _player.jumpCount = 0;
    }

    public override void Update()
    {
        base.Update();
        if (_player.strength < 100 && _player.strength >= 0)
        {
            _player.strength += 8 * Time.deltaTime;
        }
        else if (_player.strength > 100)
        {
            _player.strength = 100;
        }
        else if (_player.strength < 0)
        {
            _player.strength = 0;
        }

        if (_player.input.MovementInputValue.x == _player.facingDir && _player.isWallDetected)
            return;
        if (_player.canMove)
        {
            _stateMachine.ChangeStateTo(_player.MoveState);
        }

        if (_player.canJump)
        {
            _stateMachine.ChangeStateTo(_player.JumpState);
        }
        if (_player.canDash)
        {
            _stateMachine.ChangeStateTo(_player.DashState);
        }

        if (!_player.isGrounded && !_player.isWallDetected)
        {
            _stateMachine.ChangeStateTo(_player.AirState);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}