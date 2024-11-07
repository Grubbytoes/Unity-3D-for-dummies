using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour, IPLayerChar
{
    const float GravityStrength = 8;
    
    // The angle, in degrees, at which the player is viewing the player character
    public float ViewAngle 
    {
        get {return viewAngle;}
        set {viewAngle = value % 360f;}
    } private float viewAngle;

    public float JumpPower = 5;
    public float MoveSpeed = 5;

    private CharacterController _charControl; 
    private Vector2 _horizontalInput;
    private float _verticalVelocity;
    private Vector3 _finalMovement;
    private float _currentAngle;
    private float _rotationalVelocity;

    void Awake()
    {
        _charControl = GetComponent<CharacterController>();
        ViewAngle = 0;
    }

    void Update()
    {
        _finalMovement = Vector3.zero;

        ApplyHorizontalMovement();
        ApplyRotation();
        ApplyGravity();

        _charControl.Move(_finalMovement);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (!ctx.started || !_charControl.isGrounded) return;

        _verticalVelocity = JumpPower;
        _finalMovement.y += JumpPower * Time.deltaTime;
    }

    public void UpdateHorizontalInput(InputAction.CallbackContext ctx)
    {
        _horizontalInput = Quaternion.Euler(0f, 0f, ViewAngle) * ctx.ReadValue<Vector2>();
    }
    
    public void OnCollect(Collectable c)
    {
        // TODO
    }

    public void ApplyRotation()
    {
        if (_horizontalInput.magnitude < 0.1f) return;

        var targetAngle = Mathf.Atan2(_horizontalInput.x, _horizontalInput.y) * Mathf.Rad2Deg;
        _currentAngle = Mathf.SmoothDamp(_currentAngle, targetAngle, ref _rotationalVelocity, 0.15f);

        transform.rotation = Quaternion.Euler(0f, _currentAngle, 0f);
    }

    private void ApplyHorizontalMovement()
    {
        var deltaSpeed = Time.deltaTime * MoveSpeed;

        _finalMovement.x += _horizontalInput.x * deltaSpeed;
        _finalMovement.z += _horizontalInput.y * deltaSpeed;
    }

    private void ApplyGravity()
    {
        if (_charControl.isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -1;
        }
        else
        {
            _verticalVelocity -= 10 * Time.deltaTime;
        }
        
        _finalMovement.y += _verticalVelocity * Time.deltaTime;
    }
}
