using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
	const float GravityStrength = 10;


    protected CharacterController CharControl;
    protected float verticalMovement;
    protected Vector3 finalMovement;

    void Awake()
    {
        CharControl = GetComponent<CharacterController>();
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