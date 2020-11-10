using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float walkSpeed = 5;
    [SerializeField]
    private float sprintSpeed = 7;
    [SerializeField]
    private float sprintLoss = 0.8F;

    [SerializeField]
    private float staminaRegain = 0.5F;
    [SerializeField]
    private float staminaDelay = 1F;
    [SerializeField]
    private float interpolation = 0.8F;

    [SerializeField]
    private Rigidbody2D playerRigibody;

    private bool regainStamina = false;

    private bool isRunning = false;

    void FixedUpdate()
    {
        print(regainStamina);
        if (!GlobalVariables.ISINDIALOGUE)
        {
            if (Input.GetKey(KeyCode.LeftShift)&&GlobalVariables.STAMINA>0 )
            {
                regainStamina = false;
                playerRigibody.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxisRaw("Horizontal") * sprintSpeed, interpolation),
                                                     Mathf.Lerp(0, Input.GetAxisRaw("Vertical") * sprintSpeed, interpolation));
                StopCoroutine(RestoreStamina());
                GlobalVariables.STAMINA-=sprintLoss;
                if(GlobalVariables.STAMINA<0)
                {
                    GlobalVariables.STAMINA = 0;
                }
                
            }
            else
            {
                // Move senteces
                playerRigibody.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxisRaw("Horizontal") * walkSpeed, interpolation),
                                                     Mathf.Lerp(0, Input.GetAxisRaw("Vertical") * walkSpeed, interpolation));
            }
            if (Input.GetAxisRaw("Horizontal") < 0 && spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0 && spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            playerRigibody.velocity = Vector2.zero;
            StopCoroutine(RestoreStamina());
        }

        if(GlobalVariables.STAMINA!=100 & regainStamina == false)
        {
            StartCoroutine(RestoreStamina());

        }
        else if(GlobalVariables.STAMINA>=100)
        {
            regainStamina = false;
            GlobalVariables.STAMINA = 100;
        }
        else if (regainStamina)
        {
            GlobalVariables.STAMINA += staminaRegain;
        }
    }

    IEnumerator RestoreStamina()
    {
        yield return new WaitForSeconds(staminaDelay);
        regainStamina = true;

    }
}
