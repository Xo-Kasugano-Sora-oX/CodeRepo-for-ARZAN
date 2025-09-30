using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Entity : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected bool isGrounded;
    [Header("Բ�ε�����")]
    protected LayerMask groundLayer;
    protected Transform groundCheck;
    protected float checkRadius = 0.2f;
    [Header("״̬��")]
    protected bool isAttacking;
    protected bool IsMoving;
    protected bool isDashing;
    protected bool isJumping;
    protected bool FacingRight = true;



    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        
    }
    
    // Update is called once per frame
    protected virtual void Update()
    {
        GroundChecker();

    }
    protected virtual void GroundChecker()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);//(���㣨������ �뾶 �� ͼ��)
    }
    protected virtual void InputChecker()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.H))
        {
            if (isGrounded)
                isAttacking = true;
        }
    }
    protected virtual void Flip()
    {
        FacingRight = !FacingRight;
        UnityEngine.Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    protected void AnimatoerControllers()
    {
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("isGrunded", isGrounded);
    }


}
