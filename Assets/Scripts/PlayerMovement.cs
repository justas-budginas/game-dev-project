using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private BoxCollider2D coll;

    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool doubleJumped = false;
    [SerializeField] private float dirX = 0f;
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float doubleJumpForce = 7f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;
    
    private enum MovementState {idle, running, jumping, falling, doubleJump}

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // X axis movement
        dirX = Input.GetAxisRaw("Horizontal");
        UpdateAnimationState(dirX);
        Inputs();
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
    }


    private void Inputs()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        
        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
                jumpSoundEffect.Play();
            }
            else if (!isGrounded && !doubleJumped)
            {
                rb.AddForce(new Vector2(0f,doubleJumpForce), ForceMode2D.Impulse);
                doubleJumped = true;
                jumpSoundEffect.Play();
            }
            else
            {
                return;
            }
            
        }

        if (isGrounded)
        {
            doubleJumped = false;
        }
    }

    private void UpdateAnimationState(float dirX)
    {
        MovementState state;
        
        if (dirX > 0f)
        {
            state = MovementState.running;
            sr.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sr.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        if (rb.velocity.y > .1f && doubleJumped)
        {
            state = MovementState.doubleJump;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        
        
        anim.SetInteger("State", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
