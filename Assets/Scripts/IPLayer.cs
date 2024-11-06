using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPLayerChar
{
    public abstract void UpdateHorizontalInput(InputAction.CallbackContext ctx);

    public abstract void Jump(InputAction.CallbackContext ctx);
}