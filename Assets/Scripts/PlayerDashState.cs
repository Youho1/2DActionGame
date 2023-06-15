using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.isDashing = true;
        _player.dashTimer = _player.dashDuration;
        _player.dashCoolTimer = _player.dashCoolTime;

    }

    public override void Update()
    {
        base.Update();

        var dashVelocity = _player.dashSpeed * _player.facingDir;
        _player.SetVelocity(dashVelocity, 0);
        if (_player.dashTimer < 0)
        {
            if (_player.isGrounded)
            {
                _stateMachine.ChangeStateTo(_player.IdleState);
            }
            else
            {
                _stateMachine.ChangeStateTo(_player.AirState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        _player.SetVelocity(0, rb.velocity.y);
        _player.isDashing = false;
    }
}
