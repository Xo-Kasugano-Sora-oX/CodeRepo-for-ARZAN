    using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using UnityEngine.Rendering.Universal;
using TMPro;
public class PlayerController : MonoBehaviour

{
    [Header("test1")]
    public Rigidbody2D rb;
    [Header("test anim")]
    [SerializeField] private Animator PAnim;
    [SerializeField] private bool IsMoving;
    [SerializeField] private bool isDashing;
    [SerializeField] private bool isJumping;

    [Header("Move control")]
    private bool FacingRight = true;
    private float xInput;
    public float MoveSpeedPlayer = 4f;
    public float acceleration = 10f;
    public float maxSpeed = 8f;
    //"JUMP"
    public float JumpForce = 8f;
    public float JumpCount = 1f;
    public float JumpCounter;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    [Header("Attack Settings")]
    private bool isAttacking;
    private int combocounter;

    void Start()
    {
        PAnim = GetComponentInChildren<Animator>();
    }
    void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(new Vector2(xInput * acceleration, 0), ForceMode2D.Force);
        }
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
        //水平移动
        rb.velocity = new Vector2(MoveSpeedPlayer * 2 * xInput, rb.velocity.y);

        // 新增跳跃状态检测
        if (!isGrounded)
        {
            PAnim.SetBool("isJumping", isJumping = true);
        }
        else
        {
            PAnim.SetBool("isJumping", isJumping = false);
            if (rb.velocity.x > 0 || rb.velocity.x < 0)
            {
                PAnim.SetBool("IsMoving", IsMoving = true);
            }
            else
            {
                PAnim.SetBool("IsMoving", IsMoving = false);
            }
        }
        // 跳跃
        if (Input.GetKeyDown(KeyCode.J) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce * 2);
            JumpCounter -= 1f;
        }
        //地面检查
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);//(监测点（父）， 半径 ， 图层)

        AnimatoerControllers();
        InputChecker();
    }
    private float RushTime = 2f;
    private float RushTimer;

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

    //SKILL 1 冲刺
    [Header("DASH INFO")]
    [SerializeField] private float DashForce = 0.4f;
    [SerializeField] private float DashTime = 1f;
    [SerializeField] private float DashCoolDown = 0.5f;
    [SerializeField] private float Dashtimer = 5f;


    private void AnimatoerControllers()
    {
        PAnim.SetBool("isAttacking", isAttacking);
        PAnim.SetInteger("combocounter", combocounter);
        PAnim.SetBool("isGrunded", isGrounded);
    }
    //atack
    public void AttackOver()
    {
        isAttacking = false;
    }
    void InputChecker()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isAttacking = true;
            
        }
        xInput = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f ? Input.GetAxis("Horizontal") : 0f;
    }

}