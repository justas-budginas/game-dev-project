using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Rigidbody2D rb;
    public LayerMask groundLayers;
    public Transform groundCheck;
    private bool isFacingRight = false;
    private RaycastHit2D hitGround;
    private RaycastHit2D hitWall;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        hitGround = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);
        hitWall = Physics2D.Raycast(groundCheck.position, -transform.right, 1f, groundLayers);
    }

    private void FixedUpdate()
    {
        GroundCheck();
        WallCheck();
    }

    private void GroundCheck()
    {
        if (hitGround.collider != false) {
            if (isFacingRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
        else
        {
            Flip();
        }
    }

    private void WallCheck()
    {
        if (hitWall.collider != false)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
    }
}
