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
using System.Numerics;
public class PlayerController : Entity

{
    [Header("Move control")]
    private float xInput;
    public float MoveSpeedPlayer = 4f;
    public float acceleration = 10f;
    public float maxSpeed = 8f;
    //"JUMP"
    public float JumpForce = 8f;
    public float JumpCount = 1f;
    public float JumpCounter;
    [Header("Attack Settings")]
    public int combocounter;
    public float combocounte;
    
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
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
        rb.velocity = new UnityEngine.Vector2(MoveSpeedPlayer * 2 * xInput, rb.velocity.y);
        //冲刺/闪现

        // 新增跳跃状态检测
        if (!isGrounded)
        {
            anim.SetBool("isJumping", isJumping = true);
            if (JumpCounter! > 0)
            {
                JumpCounter -= 1;
            }
        }
        else
        {
            if (JumpCounter <= 0)
            {
                JumpCounter = JumpCount;
            }
            anim.SetBool("isJumping", isJumping = false);
            if (rb.velocity.x > 0 || rb.velocity.x < 0)
            {
                anim.SetBool("IsMoving", IsMoving = true);
            }
            else
            {
                anim.SetBool("IsMoving", IsMoving = false);
            }
        }
        // 跳跃
        if (Input.GetKeyDown(KeyCode.J) && JumpCounter!=0)
        {
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, JumpForce * 2);
            JumpCounter -= 1f;
        }
        //地面检查
        AnimatoerControllers();
        InputChecker();
    }
    public void AttackOver()
    {
        isAttacking = false;
    }
    //atack
    public bool CanJump;
}