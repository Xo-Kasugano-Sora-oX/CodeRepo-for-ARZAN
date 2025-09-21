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
    [Header("Attack Settings")]
    private bool isAttacking;
    private Animator anim;


    
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




        // 跳跃缓冲
        if (Input.GetKeyDown(KeyCode.J) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce * 2);
        }
        //地面检查（下一步）
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);//(监测点（父）， 半径 ， 图层)

        //冲刺！！！！！！

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