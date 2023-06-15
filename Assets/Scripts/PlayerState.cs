using UnityEngine;

public abstract class PlayerState
{
    protected Player _player;
    protected PlayerStateMachine _stateMachine;
    private string _animBoolName;
    protected Rigidbody2D rb;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this._player = player;
        this._stateMachine = stateMachine;
        this._animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        _player.animator.SetBool(_animBoolName, true);
        rb = _player.rb;
    }

    public virtual void Update()
    {
        _player.animator.SetFloat("yVelocity", rb.velocity.y);

    }

    public virtual void Exit()
    {
        _player.animator.SetBool(_animBoolName, false);
    }
}