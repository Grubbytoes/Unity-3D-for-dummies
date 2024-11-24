using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerCharacter : BasePlayerCharacter
{
    const float GravityStrength = 10;

    [SerializeField] private Transform ViewCamera;
    public float JumpPower = 5;
    public float MoveSpeed = 5;

    private float verticalVelocity;
    private Vector3 finalMovement;

    void Update()
    {
        finalMovement = Vector3.zero;

        ApplyHorizontalMovement();
        ApplyRotation();
        ApplyJump();
        ApplyGravity();

        charControl.Move(finalMovement);
    }

    // Apply a component along the XZ plane based on the horizontal input
    private void ApplyHorizontalMovement()
    {
        var deltaSpeed = Time.deltaTime * MoveSpeed;
        var moveDir = Quaternion.Euler(0f, 0f, -ViewCamera.eulerAngles.y) * horizontalInput;
        
        finalMovement.x += moveDir.x * deltaSpeed;
        finalMovement.z += moveDir.y * deltaSpeed;
    }

    // Rotates the character such that they are facing towards the last horizontal input
    public void ApplyRotation()
    {
        if (horizontalInput.magnitude < 0.1f) return;

        Quaternion toRotate = Quaternion.LookRotation(new Vector3(horizontalInput.x, 0f, horizontalInput.y));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, 400f * Time.deltaTime);
    }

    private void ApplyJump()
    {
        if (!doJump) return;
        doJump = false;

        verticalVelocity = JumpPower;
        finalMovement.y += JumpPower * Time.deltaTime;
    }

    // Applies downward acceleration when airborne
    private void ApplyGravity()
    {
        if (charControl.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -1;
        }
        else
        {
            verticalVelocity -= 10 * Time.deltaTime;
        }

        finalMovement.y += verticalVelocity * Time.deltaTime;
    }
}
