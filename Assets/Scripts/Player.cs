using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Move info")]
    // Move info
    public Vector2 movement;
    public float runSpeed;
    public float dashDuration;
    public float dashTimer;
    public float dashSpeed;
    public float dashCoolTime;
    public float dashCoolTimer;
    [Header("Jump info")]
    // Jump info
    public float jumpForce;

    public int jumpCount = 0;
    private const int jumpCountLimit = 2;
    public int jumpCost { get; private set; } = 20;
    public float strength = 100;
    #region collsion values
    [Header("Collision info")]
    // Collision info
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;
    public bool wallDetected;


    #endregion
    #region PlayerState

    public PlayerIdleState IdleState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerWallSlideState WallSlide { get; private set; }
    public PlayerWallJumpState WallJump { get; private set; }
    [Header("Player Current State")]
    // Player Current State
    public bool isAttacking;

    public bool isAir;
    public bool isMoving;
    public bool isDashing;
    public bool isGrounded;
    public bool isBusy;
    public bool isWallDetected;
    #endregion
    #region component
    public PlayerStateMachine StateMachine { get; private set; }
    public APlayerInput input { get; private set; }
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

    #endregion
    #region action

    [Header("Action Check")]
    // Player Action Check
    public bool canJump;

    public bool canAttack;
    public bool canDash;
    public bool canMove;

    #endregion

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    private void Awake()
    {
        #region instance player state
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        AttackState = new PlayerAttackState(this, StateMachine, "Attack");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallSlide = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJump = new PlayerWallJumpState(this, StateMachine, "Jump");
        #endregion

        #region GetComponent
        input = GetComponent<APlayerInput>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        #endregion

    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CheckAction();
        CheckCollision();
        StateMachine.CurrentState.Update();
        FlipController(movement.x);
    }

    private void CheckAction()
    {
        dashCoolTimer -= Time.deltaTime;
        dashTimer -= Time.deltaTime;
        canJump = !isAttacking && !isDashing && input.IsJumpButtonPressed && jumpCount < jumpCountLimit && strength > jumpCost - 1;
        canAttack = isGrounded && !isAttacking && !isDashing && input.IsAttackButtonPressed;
        canDash = input.IsDashButtonPressed && !isDashing && !isAttacking && dashCoolTimer < 0;
        canMove = input.IsMoveButtonPressed && !isDashing && !isAttacking && isGrounded && !isWallDetected;
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float xInputValue)
    {
        if (isDashing) return;
        if (isWallDetected) return;
        var playerIsRightMoveing = xInputValue > 0;
        var playerIsLeftMoveing = xInputValue < 0;
        if (playerIsRightMoveing && !facingRight)
            Flip();
        else if (playerIsLeftMoveing && facingRight)
            Flip();
    }
    private void CheckCollision()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsWall);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }



}