using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniffMode : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(GlobalVariables.ISINSNIFFMODE)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}
