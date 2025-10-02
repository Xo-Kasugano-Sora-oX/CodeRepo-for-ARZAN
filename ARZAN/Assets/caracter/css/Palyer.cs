using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
public class Palyer : MonoBehaviour
{
    [SerializeField] PLAYERinput input;
    Rigidbody2D rb;

    public bool FacingRight;

    private void OnEnable()
    {
        input.Wcnmd += Move;
        input.sbZhaodi += StopMove;
    }
    private void OnDisable()
    {
        input.Wcnmd -= Move;
        input.sbZhaodi -= StopMove;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        input.EnableAllInputs();
    }
    private void Update()
    {
        if ((rb.velocity.x > 0 && !FacingRight) || (rb.velocity.x < 0 && FacingRight))
        {
            Flip();
        }
    }
    void Move(Vector2 moveinput)
    {
        //Vector2 moveSpeed = moveinput * 5f;
        rb.velocity = moveinput * 5f;
    }
    void StopMove()
    {

    }

    void Flip()
    {
        FacingRight = !FacingRight;
        UnityEngine.Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}
