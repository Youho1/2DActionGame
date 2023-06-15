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

    }

    public override void Exit()
    {
        base.Exit();
        _player.isAir = false;
    }
}
