using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.jumpCount = 1;
    }

    public override void Update()
    {
        base.Update();
        _player.strength -= Time.deltaTime;
        if (_player.input.MovementInputValue.y < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);
        }

        if (_player.input.IsMoveButtonPressed && _player.facingDir != _player.input.MovementInputValue.x && _player.input.MovementInputValue.y == 0)
            _stateMachine.ChangeStateTo(_player.AirState);
        if (_player.isGrounded)
            _stateMachine.ChangeStateTo(_player.IdleState);
        if (_player.canJump)
        {
            _stateMachine.ChangeStateTo(_player.WallJump);
            return;
        }

        if (_player.strength <= 0)
        {
            _player.strength = 0;
            _stateMachine.ChangeStateTo(_player.AirState);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
