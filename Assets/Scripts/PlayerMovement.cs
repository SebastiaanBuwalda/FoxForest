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
    private float interpolation = 0.8F;

    [SerializeField]
    private Rigidbody2D playerRigibody;

    void FixedUpdate()
    {
        if (!GlobalVariables.ISINDIALOGUE)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerRigibody.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxisRaw("Horizontal") * sprintSpeed, interpolation),
                                                     Mathf.Lerp(0, Input.GetAxisRaw("Vertical") * sprintSpeed, interpolation));
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
        }
    }
}
