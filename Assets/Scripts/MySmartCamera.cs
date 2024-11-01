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
            Move(_offCenter - _offCenter.normalized * 1);
            _action = Action.RECENTER;
        }
        else if (_action == Action.FOLLOW) _action = Action.RECENTER;

        if (_action == Action.RECENTER && _offCenter.magnitude > 0.1f)
        {
            Vector3 toMove = _offCenter;
            if (toMove.magnitude > RecenterSpeed) toMove = toMove.normalized * RecenterSpeed;
            Move(toMove * Time.deltaTime);
        }
        else
        {
            _action = Action.STILL;
        }
    }

    private void Move(Vector3 toMove, float multiplier = 1)
    {
        this.transform.position += toMove * multiplier;
        _center += toMove;
    }
}