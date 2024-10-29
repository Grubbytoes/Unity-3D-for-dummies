using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController CharControl;
    public float Speed = 5f;

    PlayerInput playerInput;
    InputAction moveAction;
    private Vector2 moveVector;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Movement");
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>().normalized;
        Debug.Log(moveVector);

        if (moveVector.magnitude > 0.1f)
        {
            // Rotating
            float targetAngle = Mathf.Atan2(moveVector.x, moveVector.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            // Moving
            CharControl.Move(new Vector3(moveVector.y, 0f, moveVector.x) * 10 * Time.deltaTime);
        }
    }
}
