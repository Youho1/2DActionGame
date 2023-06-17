using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.isAir = true;
    }

    public override void Update()
    {
        base.Update();

        if (_player.isGrounded)
        {
            _stateMachine.ChangeStateTo(_player.IdleState);
        }
        if (_player.canDash)
        {
            _stateMachine.ChangeStateTo(_player.DashState);
        }
        if (_player.input.MovementInputValue.x != 0)
        {
            _player.SetVelocity(_player.runSpeed * .8f * _player.movement.x, rb.velocity.y);
        }

        if (_player.canJump)
        {
            _stateMachine.ChangeStateTo(_player.JumpState);
        }
        if (_player.isWallDetected && _player.strength > 0)
        {
            _stateMachine.ChangeStateTo(_player.WallSlide);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
