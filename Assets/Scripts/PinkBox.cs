using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinkBox : MonoBehaviour
{
    const float GravityStrength = 8;

    public float JumpPower = 5;
    public float MoveSpeed = 5;

    public InputMaster InputMaster;
    private CharacterController CharControl;
    
    private float verticalMotion;
    private Vector3 charVelocity;
    private bool queueJump;

    void Awake()
    {
        InputMaster = new InputMaster();
        CharControl = GetComponent<CharacterController>();

        InputMaster.Enable();
        InputMaster.Movement.Jump.performed += ctx => QueueJump(ctx);
    }

    void Update()
    {
        charVelocity = Vector3.zero;
        
        ApplyGravity();
        ApplyHPlaneMovement();
        ApplyJump();

        // Thou shalt not apply deltaTime in vain
        CharControl.Move(charVelocity);
    }

    void ApplyHPlaneMovement()
    {
        Vector2 HPlaneDir = InputMaster.FindAction("HPlaneMovement").ReadValue<Vector2>().normalized;

        if (HPlaneDir.magnitude > 0.1)
        {
            charVelocity += new Vector3(HPlaneDir.x, 0f, HPlaneDir.y) * MoveSpeed * Time.deltaTime;
        }
    }

    void ApplyGravity()
    {
        if (CharControl.isGrounded)
        {
            verticalMotion = -1f;
        }
        else
        {
            verticalMotion -= GravityStrength * Time.deltaTime;
        }

        charVelocity.y = verticalMotion * Time.deltaTime;
    }

    void ApplyJump()
    {
        if (!queueJump)
        {
            return;
        }

        charVelocity.y += JumpPower * Time.deltaTime;
        verticalMotion = JumpPower;
        queueJump = false;
    }

    void QueueJump(InputAction.CallbackContext ctx)
    {
        if (!CharControl.isGrounded) return;
        queueJump = true;
    }

    void OnDisable()
    {
        if (InputMaster is null) return;
        InputMaster.Disable();
    }
}
