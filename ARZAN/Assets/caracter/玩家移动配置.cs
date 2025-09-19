    using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
public class PlayerController : MonoBehaviour

{
    [Header("test1")]
    public Rigidbody2D rb;
    [Header("test anim")]
    [SerializeField] private Animator PAnim;

    [Header("Move control")]
    private bool FacingRight = true;
    public bool SHIT1ON;
    private float xInput;
    public float MoveSpeedPlayer = 4f;
    public float acceleration = 10f;
    public float maxSpeed = 8f;
    private float velocityY;
    //"JUMP"
    public float JumpForce = 8f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float checkRadius = 0.2f;
        [Header("Dash Settings")]
    public float dashForce = 25f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public LayerMask dashStopLayers;
    public Image cooldownImage;
    private Animator anim;
    private float lastDashTime = -10f;
    private bool isDashing;
    void Start()
    {
        PAnim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        //检测是否向右移动
        float move = Input.GetAxis("Horizontal");
        if (move > 0 && !FacingRight)
        {
            Flip();
        }
        else if (move < 0 && FacingRight)
        {
            Flip();
        }

        /*从这里*/
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            JumpForce = JumpForce + 1f;
            Debug.Log("+1");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            JumpForce = JumpForce - 1f;
            Debug.Log("-1");
        }
        /*到这里，为了方便调试特地加的*/

        //获得按键输入为A、D分别输出-1、1
        xInput = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f ? Input.GetAxis("Horizontal") : 0f;

        rb.velocity = new Vector2(MoveSpeedPlayer * 2 * xInput, rb.velocity.y);


        if (Input.GetKeyDown(KeyCode.J))//跳跃按键检测
        {
            Debug.Log("你是不是尝试按跳跃键了");
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }

        // 原有速度检测逻辑
        if (rb.velocity.x > 5f)
        {
            PAnim.SetBool("isRushing", isRushing = true);
        }
        else
        {
            PAnim.SetBool("isRushing", isRushing = false);

            // 新增跳跃状态检测
            if (!isGrounded)
            {
                PAnim.SetBool("isJumping", isJumping = true);
            }
            else
            {
                PAnim.SetBool("isJumping", isJumping = false);
                // 原有移动状态检测...
            }
        }


        // 跳跃缓冲
        if (Input.GetKeyDown(KeyCode.J))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0 && isGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            jumpBufferCounter = 0;
        }
        //地面检查（下一步）
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);//(监测点（父）， 半径 ， 图层)

        if (Input.GetKeyDown(KeyCode.J) && isGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
        //冲刺！！！！！！
        UpdateCooldownUI();
        
        if(Input.GetKeyDown(KeyCode.K) && CanDash()) {
            StartCoroutine(PerformDash());
        }
    }
    
    bool CanDash() {
        return Time.time > lastDashTime + dashCooldown && !isDashing;
    }

    IEnumerator PerformDash()
    {
        if (rb == null || PAnim == null)
        {
            Debug.LogError("Required components are not assigned!");
            yield break;
        }

        isDashing = true;
        lastDashTime = Time.time;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        anim.SetBool("isDashing", isDashing = true);

        int direction = transform.localScale.x > 0 ? 1 : -1;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(direction * dashForce, 0),
                   ForceMode2D.Impulse);

        float elapsed = 0f;
        while (elapsed < dashDuration)
        {
            if (Physics2D.OverlapCircle(transform.position, 0.5f, dashStopLayers))
            {
                break;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.gravityScale = originalGravity;
        rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y);
        anim.SetBool("isDashing", false);
        isDashing = false;
    }
    //UI
    void UpdateCooldownUI() {
        if(cooldownImage != null) {
            float cooldownProgress = Mathf.Clamp01(
                (Time.time - lastDashTime) / dashCooldown);
            cooldownImage.fillAmount = 1 - cooldownProgress;
        }
    }
    
    private bool isGrounded;//布尔值判断真假
    //实现换方向时人物翻转
    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    //animations
    [SerializeField] private bool IsMoving;
    [SerializeField] private bool isRushing;
    [SerializeField] private bool isJumping;

    //SKILL 1 冲刺
    [Header("DASH INFO")]
    [SerializeField] private float DashForce = 0.4f;
    [SerializeField] private float DashTime = 1f;
    [SerializeField] private float DashCoolDown = 0.5f;
    [SerializeField] private float Dashtimer = 5f;

    //移动物理优化
    void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(new Vector2(xInput * acceleration, 0), ForceMode2D.Force);
        }
    }
    //跳跃缓冲
    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;


}