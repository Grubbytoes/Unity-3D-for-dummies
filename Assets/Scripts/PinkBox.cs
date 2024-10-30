using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinkBox : MonoBehaviour
{
    private InputMaster inputMaster;
    private CharacterController charControl;
    private float gravStrength = 8;
    private float verticalVelocity;
    private Vector3 finalMoveDir;
    private bool _queueJump;

    void Awake()
    {
        inputMaster = new InputMaster();
        charControl = GetComponent<CharacterController>();

        inputMaster.Enable();
        inputMaster.Movement.Jump.performed += ctx => QueueJump();
    }

    void Update()
    {
        finalMoveDir = new Vector3();
        
        // State altering stuff
        ApplyHPlaneMovement();
        ApplyGravity();
        ApplyJump();

        // Extra maths
        finalMoveDir.y = verticalVelocity * Time.deltaTime;

        // Thou shalt not apply deltaTime in vain
        charControl.Move(finalMoveDir);
    }

    void ApplyHPlaneMovement()
    {
        Vector2 HPlaneDir = inputMaster.FindAction("HPlaneMovement").ReadValue<Vector2>().normalized;

        if (HPlaneDir.magnitude > 0.1)
        {
            finalMoveDir += new Vector3(HPlaneDir.x, 0f, HPlaneDir.y) * 5 * Time.deltaTime;
        }
    }

    void ApplyGravity()
    {
        if (charControl.isGrounded)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity -= gravStrength * Time.deltaTime;
        }
    }

    void ApplyJump()
    {
        if (!_queueJump)
        {
            return;
        }

        verticalVelocity = 5;

        _queueJump = false;
    }

    void QueueJump()
    {
        if (charControl.isGrounded)
        {
            _queueJump = true;
        }
    }

    void OnDisable()
    {
        inputMaster.Disable();
    }
}
