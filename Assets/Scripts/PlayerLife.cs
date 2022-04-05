using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private bool isDead = false;
    private bool oneTime = false;
    private bool halfDead = false;

    private PlayerMovement pm;
    
    [SerializeField] private int livesCount = 3;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private Text livesText;
    [SerializeField] private Text collectiblesText;
    [SerializeField] private Text gameOverText;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        livesText.text = "Lives: " + livesCount;
        gameOverText.enabled = false;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -7.5f && !halfDead)
        {
            Die();
            halfDead = true;
        }
    }

    private void Update()
    {
        if (isDead && Input.GetButtonDown("Jump"))
        {
            RestartLevel();
        }
        
        if (livesCount <= 0)
        {
            isDead = true;
        }

        if (isDead)
        {
            if (!oneTime)
            {
                FinalDeath();
                oneTime = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("Death");
        rb.bodyType = RigidbodyType2D.Static;
        deathSoundEffect.Play();
    }

    private void Respawn()
    {
        anim.Play("Player_Spawn");
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.position = respawnPoint.transform.position;
        livesCount--;
        livesText.text = "Lives: " + livesCount;
        halfDead = false;
    }

    private void FinalDeath()
    {
        rb.bodyType = RigidbodyType2D.Static;
        deathSoundEffect.Play();
        anim.Play("Player_Death_Final");
        livesText.enabled = false;
        collectiblesText.enabled = false;
        gameOverText.enabled = true;
        pm.enabled = false;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
