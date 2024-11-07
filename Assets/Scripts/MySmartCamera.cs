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

    public PlayerCharacter ViewedChar;
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
    
    private void Update() 
    {
        // Update the target position
        _targetOffMark = Target.position - _targetMark;
    }
    void LateUpdate()
    {
        TrackTarget();

        if (_controlDir.x != 0) XOrbit(_controlDir.x * 120f * Time.deltaTime);
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

    void XOrbit(float amount)
    {
        var toRotate = Quaternion.Euler(0f, amount, 0f);
        _positionRToMark = toRotate * _positionRToMark;
        transform.rotation = toRotate * transform.rotation;
        transform.position = _targetMark + _positionRToMark;

        // I hate this
        // But I honest to god cannot think of a better way
        if (ViewedChar != null) ViewedChar.ViewAngle -= amount;
    }

    void ExtendDolly(float amount)
    {
        var toMove = _targetOffMark.normalized * amount;
        this.transform.position += toMove;
    }

    public void UpdateControlDir(InputAction.CallbackContext context)
    {
        _controlDir = context.ReadValue<Vector2>();
    }


    void AbsoluteMove(Vector3 toMove)
    {
        this.transform.position += toMove;
        _targetMark += toMove;
    }
}