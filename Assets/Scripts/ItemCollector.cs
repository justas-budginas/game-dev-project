using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int collected = 0;
    [SerializeField] private AudioSource collectSoundEffect;

    [SerializeField] private Text collectiblesText;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Collectible"))
        {
            Destroy(col.gameObject);
            collected++;
            collectiblesText.text = "Collectibles: " + collected;
            collectSoundEffect.Play();
        }
    }
}
