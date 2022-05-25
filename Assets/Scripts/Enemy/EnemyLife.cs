using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audio;
    private BoxCollider2D bcd;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();
        audio = transform.GetComponent<AudioSource>();
        bcd = transform.GetComponent<BoxCollider2D>();
    }

    public void Die()
    {
        bcd.isTrigger = false;
        audio.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("die");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
