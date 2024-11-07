using System;
using UnityEngine;
using UnityEngine.InputSystem;

class MySmartCamera : MonoBehaviour
{
    enum Action
    {
        STILL,
        FOLLOW,
        RECENTER
    }

    public Transform Target;
    public float RecenterSpeed;
    public float Dolly { get { return _targetOffset.magnitude; } }

    private Vector3 _targetOffset;
    private Vector3 _center;
    private Vector3 _offCenter;
    private Action _action;

    void Start()
    {
        _offCenter = Vector3.zero;
        _center = Target.position;
        _targetOffset = this.transform.position - Target.position;
        _action = Action.STILL;
    }

    void LateUpdate()
    {
        // _offCenter always updates, don't worry!
        _offCenter = Target.position - _center;

        if (_offCenter.magnitude > 1)
        {
            AbsoluteMove(_offCenter - _offCenter.normalized * 1);
            _action = Action.RECENTER;
        }
        else if (_action == Action.FOLLOW) _action = Action.RECENTER;

        if (_action == Action.RECENTER && _offCenter.magnitude > 0.1f)
        {
            Vector3 toMove = _offCenter;
            if (toMove.magnitude > RecenterSpeed) toMove = toMove.normalized * RecenterSpeed;
            AbsoluteMove(toMove * Time.deltaTime);
        }
        else
        {
            _action = Action.STILL;
        }
    }

    public void Orbit(InputAction.CallbackContext context)
    {
        Debug.Log("I should be orbiting!");
    }

    void ExtendDolly(float amount, float multiplier = 1)
    {
        var toMove = _offCenter.normalized * amount * multiplier;
        this.transform.position += toMove;
    }

    private void AbsoluteMove(Vector3 toMove, float multiplier = 1)
    {
        this.transform.position += toMove * multiplier;
        _center += toMove;
    }
}