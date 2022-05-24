using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollected : MonoBehaviour
{
    Material material;
    
    bool isDissolving = false;
    private bool isFlashing = false;
    float fade = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Dissolve();
        Flash();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (material.GetFloat("_ID") == 0)
            {
                isDissolving = true;
            }

            if (material.GetFloat("_ID") == 1)
            {
                Debug.Log("Flash");
                isFlashing = true;
            }
            
        }
    }

    public void Flash()
    {
        if (isFlashing)
        {
            StartCoroutine(Flashing());
            isFlashing = false;
        }
    }
    

    public void Dissolve()
    {
        if (isDissolving)
        {
            fade -= Time.deltaTime;

            if (fade <= 0f)
            {
                fade = 0f;
                isDissolving = false;
                Destroy(transform.gameObject);
            }

            // Set the property
            material.SetFloat("_Fade", fade);
        }
    }
    
    IEnumerator Flashing()
    {
        material.SetFloat("_Alpha", 0);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.5f);
        material.SetFloat("_Alpha", 1);
        yield return new WaitForSeconds(0.5f);
        material.SetFloat("_Alpha", 0);
        yield return new WaitForSeconds(0.5f);
        material.SetFloat("_Alpha", 1);
        yield return new WaitForSeconds(0.5f);
        material.SetFloat("_Alpha", 0);
        Destroy(transform.gameObject);
    }
}
