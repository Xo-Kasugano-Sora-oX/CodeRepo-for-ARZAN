using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
public class Palyer : MonoBehaviour
{
    [SerializeField] PLAYERinput input;
    [SerializeField] GameObject pblt;
    [SerializeField] Transform pgun;
    Rigidbody2D rb;

    public bool FacingRight = true;

    public Vector2 movespeed;

    private void OnEnable()
    {
        input.Wcnmd += Move;
        input.sbZhaodi += StopMove;
        input.Sbzhaodi += StopFire;
        input.sbzhaoDi += Fire;
    }
    private void OnDisable()
    {
        input.Wcnmd -= Move;
        input.sbZhaodi -= StopMove;
        input.Sbzhaodi -= StopFire;
        input.sbzhaoDi -= Fire;
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
    #region Move

    void Move(Vector2 moveinput)
    {
        movespeed = moveinput * 5f;
        rb.velocity = movespeed;
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
    #endregion
    #region Fire
    void Fire()
    {
        Instantiate(pblt, pgun.position ,Quaternion.identity);
    }
    void StopFire()
    {

    }
    #endregion
}
