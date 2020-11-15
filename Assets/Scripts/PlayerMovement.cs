using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float walkSpeed = 5;
    [SerializeField]
    private float sprintSpeed = 7;
    [SerializeField]
    private float sprintLoss = 0.4F;

    [SerializeField]
    private float sniffLoss = 0.2F;

    [SerializeField]
    private float staminaRegain = 0.5F;
    [SerializeField]
    private float staminaDelay = 1F;
    [SerializeField]
    private float interpolation = 0.8F;

    [SerializeField]
    private Rigidbody2D playerRigibody;

    private bool regainStamina = false;

    private bool isNotRecoveringStamina = true;



    [SerializeField]
    private Text staminaText;

    void FixedUpdate()
    {
        if (!GlobalVariables.ISINDIALOGUE)
        {
            if(Input.GetKey(KeyCode.Q) && GlobalVariables.STAMINA > 0 && isNotRecoveringStamina)
            {
                regainStamina = false;
                StopCoroutine(RestoreStamina());

                GlobalVariables.ISINSNIFFMODE = true;
                GlobalVariables.STAMINA -= sniffLoss;
                UpdateStaminaText();
                if (GlobalVariables.STAMINA < 0)
                {
                    GlobalVariables.STAMINA = 0;
                    isNotRecoveringStamina = false;
                    GlobalVariables.ISINSNIFFMODE = false;

                    UpdateStaminaText();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                GlobalVariables.ISINSNIFFMODE = false;
            }
            if (Input.GetKey(KeyCode.LeftShift)&&GlobalVariables.STAMINA>0&&isNotRecoveringStamina&&(Input.GetAxisRaw("Horizontal")!=0||Input.GetAxisRaw("Vertical") != 0))
            {
                regainStamina = false;
                playerRigibody.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxisRaw("Horizontal") * sprintSpeed, interpolation),
                                                     Mathf.Lerp(0, Input.GetAxisRaw("Vertical") * sprintSpeed, interpolation));
                StopCoroutine(RestoreStamina());
                GlobalVariables.STAMINA-=sprintLoss;
                UpdateStaminaText();
                if(GlobalVariables.STAMINA<0)
                {
                    GlobalVariables.STAMINA = 0;
                    isNotRecoveringStamina = false;
                    GlobalVariables.ISINSNIFFMODE = false;

                    UpdateStaminaText();
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
            GlobalVariables.ISINSNIFFMODE = false;
        }

        if(GlobalVariables.STAMINA!=100 & regainStamina == false & GlobalVariables.ISINDIALOGUE == false)
        {
            StartCoroutine(RestoreStamina());

        }
        else if(GlobalVariables.STAMINA>=100)
        {
            isNotRecoveringStamina = true;
            regainStamina = false;
            GlobalVariables.STAMINA = 100;
            UpdateStaminaText();

        }
        else if (regainStamina)
        {
            GlobalVariables.STAMINA += staminaRegain;
            UpdateStaminaText();

        }
    }

    IEnumerator RestoreStamina()
    {
        yield return new WaitForSeconds(staminaDelay);
        regainStamina = true;

    }

    void UpdateStaminaText()
    {
        staminaText.text = Mathf.Round(GlobalVariables.STAMINA/10).ToString();
        if(isNotRecoveringStamina)
        {
            staminaText.color = Color.black;
        }
        else
        {
            staminaText.color = Color.red;
        }
    }
}
