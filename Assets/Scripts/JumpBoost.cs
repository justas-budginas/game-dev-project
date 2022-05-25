using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    public float jumpBoost = 7f;
    private Animator anim;
    private BoxCollider2D bcd;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        bcd = GetComponent<BoxCollider2D>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(IsOnTop(collision))
            {
                anim.SetTrigger("boostTrigger");
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpBoost, ForceMode2D.Impulse);
            }
            else
            {
                bcd.isTrigger = true;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bcd.isTrigger = false;
        }
    }

    private bool IsOnTop(Collision2D collision)
    {
        Vector3 direction = transform.position - collision.gameObject.transform.position;

        if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y < 0)
                return true;
        }
        return false;
    }
}
