using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BasePlayerCharacter : MonoBehaviour
{
    // The angle, in degrees, at which the player is viewing the player character
    private float _viewAngle;
    public float ViewAngle
    {
        get { return _viewAngle; }
        set { _viewAngle = value % 360f; }
    }

    // The players inventory, storing references to collectable items
    private ItemStore _inventory;
    public ItemStore Inventory {
        get => _inventory; 
        private set => _inventory = value;
    }
    
    protected CharacterController charControl; 
    protected Vector2 horizontalInput;
    protected bool doJump;

    void Awake()
    {
        charControl = GetComponent<CharacterController>();
        ViewAngle = 0;
        Inventory = new ItemStore();
    }

    // Receives the Vector2 representing the players arrow key input, rotated by the view angle
    public void HorizontalInput(InputAction.CallbackContext ctx)
    {
        horizontalInput = Quaternion.Euler(0f, 0f, ViewAngle) * ctx.ReadValue<Vector2>();
    }

    // Receives the call to jump
    // Adds an upwards component to final movement and vertical velocity
    public void JumpInput(InputAction.CallbackContext ctx)
    {
        if (!ctx.started || !charControl.isGrounded) return;
        doJump = true;
    }

    public abstract void OnCollect(Collectable collectable);
}