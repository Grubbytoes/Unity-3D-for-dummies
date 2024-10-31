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

    private CharacterController CharControl; 

    private Vector2 _horizontalInput;
    private float _verticalVelocity;
    private Vector3 _finalMovement;

    void Awake()
    {
        CharControl = GetComponent<CharacterController>();
    }

    void Update()
    {
        _finalMovement = Vector3.zero;

        ApplyHorizontalMovement();
        ApplyGravity();

        CharControl.Move(_finalMovement);
    }

    private void ApplyHorizontalMovement()
    {
        var deltaSpeed = Time.deltaTime * MoveSpeed;

        _finalMovement.x += _horizontalInput.x * deltaSpeed;
        _finalMovement.z += _horizontalInput.y * deltaSpeed;
    }

    private void ApplyGravity()
    {
        if (CharControl.isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -1;
        }
        else
        {
            _verticalVelocity -= 10 * Time.deltaTime;
        }
        
        _finalMovement.y += _verticalVelocity * Time.deltaTime;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (!ctx.started || !CharControl.isGrounded) return;

        _verticalVelocity = JumpPower;
        _finalMovement.y += JumpPower * Time.deltaTime;
    }

    public void UpdateHorizontalInput(InputAction.CallbackContext ctx)
    {
        _horizontalInput = ctx.ReadValue<Vector2>();
    }
}
