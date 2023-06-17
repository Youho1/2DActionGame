using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    private float startTime;
    private float wallJumpHorizontalForce = 5f;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        startTime = 0.4f;
        _player.jumpCount++;
        _player.strength -= _player.jumpCost;
        _player.SetVelocity(wallJumpHorizontalForce * _player.input.MovementInputValue.x, _player.jumpForce);
    }

    public override void Update()
    {
        base.Update();
        startTime -= Time.deltaTime;
        if (startTime < 0)
            _stateMachine.ChangeStateTo(_player.AirState);
        if (_player.isGrounded)
            _stateMachine.ChangeStateTo(_player.IdleState);

    }

    public override void Exit()
    {
        base.Exit();
    }
}
