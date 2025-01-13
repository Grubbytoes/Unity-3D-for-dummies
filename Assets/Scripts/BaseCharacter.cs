using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    const float GravityStrength = 10;

    public float jumpPower = 5;
    public float moveSpeed = 5;
    public bool movementDisabled = false;

    protected CharacterController CharControl;
    protected bool DoJump;
    protected Vector2 intendedMoveDir;
    
    private Vector2 actualMoveDir;
    private float verticalMovement;
    private Vector3 finalMovement;

    void Awake()
    {
        CharControl = GetComponent<CharacterController>();
    }

    public void DisableControl()
    {
        movementDisabled = true;
    }

    public void EnableControl()
    {
        movementDisabled = false;
    }

    protected void MoveCycle()
    {
        finalMovement = Vector3.zero;

        ApplyHorizontalMovement();
        ApplyGravity();
        ApplyJump();
		ApplyRotation();

        CharControl.Move(finalMovement);
    }

    // Apply a component along the XZ plane based on the horizontal input
    // Returns early, before making any changes to _finalMovement, if IsDisabled;
    private void ApplyHorizontalMovement()
    {
        if (movementDisabled) return;

        var deltaSpeed = Time.deltaTime * moveSpeed;
        actualMoveDir = intendedMoveDir;

        finalMovement.x += actualMoveDir.x * deltaSpeed;
        finalMovement.z += actualMoveDir.y * deltaSpeed;
    }

    // Rotates the character such that they are facing towards the direction of travel
    private void ApplyRotation()
    {
        if (intendedMoveDir.magnitude < 0.1f) return;

        Quaternion toRotate = Quaternion.LookRotation(new Vector3(actualMoveDir.x, 0f, actualMoveDir.y));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, 400f * Time.deltaTime);
    }

    // Applies upward velocity on the y axis, if a jump is queued (DoJump)
    // Returns early, before making any changes to _finalMovement, if IsDisabled;
    private void ApplyJump()
    {
        if (movementDisabled) return;

        if (!DoJump) return;
        DoJump = false;

        verticalMovement = jumpPower;
        finalMovement.y += jumpPower * Time.deltaTime;
    }

    // Applies downward acceleration when airborne
    protected void ApplyGravity()
    {
        if (CharControl.isGrounded && verticalMovement < 0f)
        {
            verticalMovement = -1;
        }
        else
        {
            verticalMovement -= GravityStrength * Time.deltaTime;
        }

        finalMovement.y += verticalMovement * Time.deltaTime;
    }

}