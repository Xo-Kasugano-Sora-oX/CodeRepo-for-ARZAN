using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Entity : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    [SerializeField] protected bool isGrounded;
    [Header("µØÃæ¼à²â")]
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float checkDistance = 0.2f;
    [Header("×´Ì¬»ú")]
    [SerializeField]protected bool isAttacking;
    [SerializeField] protected bool isMoving;
    [SerializeField] protected bool isDashing;
    [SerializeField] protected bool isJumping;
    [SerializeField] protected bool FacingRight = true;



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
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance,groundLayer);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x,groundCheck.position.y - checkDistance));
    }

}