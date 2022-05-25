using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomper : MonoBehaviour
{
    private Rigidbody2D rb;
    public float boundeForcce;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hurtbox")
        {
            col.gameObject.GetComponentInParent<EnemyLife>().Die();
            rb.AddForce(transform.up * boundeForcce, ForceMode2D.Impulse);
        }
    }
}
