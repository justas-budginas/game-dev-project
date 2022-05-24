using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphics : MonoBehaviour
{
    [SerializeField] public bool highEnd = false;

    [SerializeField] private GameObject collectiblesParent;

    [SerializeField] private Material dissolve;
    [SerializeField] private Material flash;
    // Start is called before the first frame update
    void Start()
    {
        if (highEnd)
        {
            ChangeMaterial(true);
        }
        else
        {
            ChangeMaterial(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMaterial(bool isHighEnd)
    {
        if (isHighEnd)
        {
            foreach(Transform child in collectiblesParent.transform)
            {
                SpriteRenderer SR = child.GetComponent<SpriteRenderer>();
                SR.material = dissolve;
            }
        }
        else
        {
            foreach(Transform child in collectiblesParent.transform)
            {
                SpriteRenderer SR = child.GetComponent<SpriteRenderer>();
                SR.material = flash;
            }
        }
    }
}
