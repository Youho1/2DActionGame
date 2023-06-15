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
    }

    public override void Update()
    {
        base.Update();
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
    }

    public override void Exit()
    {
        base.Exit();
    }
}