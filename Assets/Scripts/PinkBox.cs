using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinkBox : MonoBehaviour
{
    public InputMaster controls;

    void Awake()
    {
        controls = new InputMaster();
        controls.Enable();
        controls.Movement.Jump.performed += ctx => DoJump();
    }

    void DoJump()
    {
        Debug.Log("boing!");
    }

    void OnDisable()
    {
        controls.Disable();
    }
}
