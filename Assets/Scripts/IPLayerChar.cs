using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPLayerChar
{
    public abstract float ViewAngle {get; set;}

    public abstract void UpdateHorizontalInput(InputAction.CallbackContext ctx);
    public abstract void Jump(InputAction.CallbackContext ctx);
    public abstract void OnCollect(Collectable collectable);
}