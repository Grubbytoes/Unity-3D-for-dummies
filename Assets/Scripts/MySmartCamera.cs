using System;
using UnityEngine;
using UnityEngine.InputSystem;

class MySmartCamera : MonoBehaviour
{
    public Transform Target;

    private Vector3 _targetOffset;

    void Start()
    {
        _targetOffset = this.transform.position - Target.position;
    }

    void LateUpdate()
    {
        this.transform.position = Target.position + _targetOffset;
    }
}