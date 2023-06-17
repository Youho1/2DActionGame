
public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.SetVelocity(rb.velocity.x, _player.jumpForce);
        _player.jumpCount++;
        _player.strength -= _player.jumpCost;
    }

    public override void Update()
    {
        base.Update();
        var playerIsFalling = rb.velocity.y < 0;
        if (playerIsFalling)
        {
            _stateMachine.ChangeStateTo(_player.AirState);
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

