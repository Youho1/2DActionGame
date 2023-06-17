using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
        stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.isMoving = true;
    }

    public override void Update()
    {
        base.Update();
        MovementExecute();
        if (!_player.canMove || _player.input.MovementInputValue.x == _player.facingDir && _player.isWallDetected)
        {
            _stateMachine.ChangeStateTo(_player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _player.isMoving = false;
    }

    private void MovementExecute()
    {
        var xVelocity = _player.movement.x * _player.runSpeed;
        _player.SetVelocity(xVelocity, rb.velocity.y);
    }
}