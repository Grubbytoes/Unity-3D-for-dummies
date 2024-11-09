using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : BasePlayerCharacter
{
    const float GravityStrength = 10;

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

        finalMovement.x += horizontalInput.x * deltaSpeed;
        finalMovement.z += horizontalInput.y * deltaSpeed;
    }

    // Rotates the character such that they are facing towards the last horizontal input
    public void ApplyRotation()
    {
        if (horizontalInput.magnitude < 0.1f) return;

        var targetAngle = Mathf.Atan2(horizontalInput.x, horizontalInput.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
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

    // Called upon picking up collectable c
    public override void OnCollect(Collectable c)
    {
        // TODO
    }
}
