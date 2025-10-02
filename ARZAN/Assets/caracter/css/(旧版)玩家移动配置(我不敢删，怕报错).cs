using UnityEngine;

public class PlayerController : Entity
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float maxSpeed = 8f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 8f;


    [Header("Attack Settings")]
    [SerializeField] private int comboCounter=1;
    [SerializeField] private int comboCount;

    [SerializeField] private float comboResetTime = 2f;

    private float xInput;

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();
        HandleInput();
        UpdateMovement();
        UpdateJump();
        UpdateAnimations();
        RestartATKcount();
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        // 攻击输入检测（示例）
        if (Input.GetKeyDown(KeyCode.H))
        {
            isAttacking = true;
            comboCounter++;
            anim.SetFloat("comboCounter", comboCount + 1);
        }
    }

    void RestartATKcount()
    {
        if (isAttacking)
        {
            comboResetTime -= Time.deltaTime;
        }
            if (comboCounter >= 3 || comboResetTime <= 0)
        {
            comboCounter = 0;
            comboResetTime = 2f;
        }
    }

    private void UpdateMovement()
    {
        // 水平移动（带加速度）
        float targetSpeed = xInput * moveSpeed;
        float speedDifference = targetSpeed - rb.velocity.x;
        float movement = speedDifference * acceleration;

        rb.AddForce(Vector2.right * movement, ForceMode2D.Force);

        // 速度限制
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        // 转向检测
        if ((xInput > 0 && !FacingRight) || (xInput < 0 && FacingRight))
        {
            Flip();
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("isMoving", isMoving = true);
        }
        else
        {
            anim.SetBool("isMoving", isMoving = false);
        }
    }


    private void UpdateJump()
    {
        if (Input.GetKeyDown(KeyCode.J) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if(!isGrounded)
        {
            anim.SetBool("isJumping", isJumping = true);
        }
        if (isGrounded)
        {
            anim.SetBool("isJumping", isJumping = false);
        }
    }
    private void UpdateAnimations()
    {
        anim.SetBool("isGrounded", isGrounded);
    }
    public void AttackOver()
    {
        isAttacking = false;
    }
}
