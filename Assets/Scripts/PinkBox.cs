using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinkBox : MonoBehaviour
{
    private InputMaster inputMaster;
    private CharacterController charControl;

    void Awake()
    {
        inputMaster = new InputMaster();
        charControl = GetComponent<CharacterController>();

        inputMaster.Enable();
        inputMaster.Movement.Jump.performed += ctx => DoJump();
    }

    void Update()
    {
        Vector2 HPlaneDir = inputMaster.FindAction("HPlaneMovement").ReadValue<Vector2>().normalized;

        if (HPlaneDir.magnitude > 0.1)
        {
            Debug.Log($"({HPlaneDir.x}, {HPlaneDir.y})");
            charControl.Move(new Vector3(HPlaneDir.x, 0f, HPlaneDir.y) * 5 * Time.deltaTime);
        }
    }

    void DoJump()
    {
        Debug.Log("boing!");
    }

    void OnDisable()
    {
        inputMaster.Disable();
    }
}
