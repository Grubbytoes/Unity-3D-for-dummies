using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

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
    public float Dolly { get { return _positionRToMark.magnitude; } }

    private Vector3 _positionRToMark;
    private Vector3 _targetMark;
    private Vector3 _targetOffMark;
    private Action _trackingAction;
    private Vector2 _controlDir;

    void Start()
    {
        _targetOffMark = Vector3.zero;
        _targetMark = Target.position;
        _positionRToMark = this.transform.position - _targetMark;
        _trackingAction = Action.STILL;
    }

    void LateUpdate()
    {
        _targetOffMark = Target.position - _targetMark;

        TrackTarget();
    }

    void TrackTarget()
    {
        if (_targetOffMark.magnitude > 1)
        {
            AbsoluteMove(_targetOffMark - _targetOffMark.normalized * 1);
            _trackingAction = Action.RECENTER;
        }
        else if (_trackingAction == Action.FOLLOW) _trackingAction = Action.RECENTER;

        if (_trackingAction == Action.RECENTER && _targetOffMark.magnitude > 0.1f)
        {
            var toMove = RecenterSpeed * Time.deltaTime;

            if (_targetOffMark.magnitude < toMove)
            {
                AbsoluteMove(_targetOffMark);
            }
            else
            {
                AbsoluteMove(_targetOffMark.normalized * toMove);
            }
        }
        else
        {
            _trackingAction = Action.STILL;
        }
    }

    void ExtendDolly(float amount, float multiplier = 1)
    {
        var toMove = _targetOffMark.normalized * amount * multiplier;
        this.transform.position += toMove;
    }

    public void UpdateControlDir(InputAction.CallbackContext context)
    {
        _controlDir = context.ReadValue<Vector2>();
    }


    void AbsoluteMove(Vector3 toMove, float multiplier = 1)
    {
        this.transform.position += toMove * multiplier;
        _targetMark += toMove;
    }
}