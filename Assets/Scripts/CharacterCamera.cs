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

    public Vector3 TargetPosition {get => targetChar.transform.position;}
    public Vector3 TargetOffMark {get => TargetPosition - targetMark;}

    public PlayerCharacter targetChar;
    public float recenterSpeed = 0.5f;
    public float maxOrbitSpeed = 120f;

    private Vector3 positionRToMark;
    private Vector3 targetMark;
    private Action trackingAction;
    private Vector2 controlDir;
    private float rotationalSpeed = 0;

    void Start()
    {
        targetMark = TargetPosition;
        positionRToMark = this.transform.position - targetMark;
        trackingAction = Action.STILL;
    }
    
    void LateUpdate()
    {
        TrackTarget();

        if (controlDir.x != 0) 
        {
            rotationalSpeed += maxOrbitSpeed * 2 * Time.deltaTime;
            rotationalSpeed = Math.Min(rotationalSpeed, maxOrbitSpeed);
            XOrbit(controlDir.x * rotationalSpeed * Time.deltaTime);
        }
        else if (rotationalSpeed > 0)
        {
            rotationalSpeed = 0;
        }
    }

    // Updates position based on how far the target is off their mark
    void TrackTarget()
    {
        if (TargetOffMark.magnitude > 1)
        {
            AbsoluteMove(TargetOffMark - TargetOffMark.normalized * 1);
            trackingAction = Action.RECENTER;
        }
        else if (trackingAction == Action.FOLLOW) trackingAction = Action.RECENTER;

        if (trackingAction == Action.RECENTER && TargetOffMark.magnitude > 0.1f)
        {
            var toMove = recenterSpeed * Time.deltaTime;

            if (TargetOffMark.magnitude < toMove)
            {
                AbsoluteMove(TargetOffMark);
            }
            else
            {
                AbsoluteMove(TargetOffMark.normalized * toMove);
            }
        }
        else
        {
            trackingAction = Action.STILL;
        }
    }

    // Orbits horizontally around the target mark
    void XOrbit(float amount)
    {
        var toRotate = Quaternion.Euler(0f, amount, 0f);
        positionRToMark = toRotate * positionRToMark;
        transform.rotation = toRotate * transform.rotation;
        transform.position = targetMark + positionRToMark;

        // I hate this
        // But I honest to god cannot think of a better way
        if (targetChar != null) targetChar.ViewAngle -= amount;
    }

    // Receiver for player input 
    public void ControlDirInput(InputAction.CallbackContext context)
    {
        controlDir = context.ReadValue<Vector2>();
    }

    // Moves the camera AND target mark position
    void AbsoluteMove(Vector3 toMove)
    {
        this.transform.position += toMove;
        targetMark += toMove;
    }
}