
using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerCharacter : BaseCharacter
{
	public static event Action CollectedEnoughItems;
	public static event Action UiEscape;

	public UnityEvent<string> ItemPickedUp;

	public readonly ItemStore Inventory = new();
	public Transform ViewCamera;
	
	[SerializeField] protected InteractHand interactHand;


	private bool _collectedEnoughAlready = false;

	void Update()
	{
		MoveCycle();
		SfxCycle();
	}

	// Called upon picking up a collectable
	public virtual void OnCollect(Collectable collectable)
	{
		ItemPickedUp.Invoke(collectable.ItemName);
		// Check whether we have collected enough
		// For now, we'll go for 12 geodes and 3 tonics
		if (Inventory.Has("geode", 1) && Inventory.Has("tonic", 1)) 
		{
			Debug.Log("Ready for cutscene!");
			if (_collectedEnoughAlready) return;
			CollectedEnoughItems();
			_collectedEnoughAlready = true;
		}
		else
		{
			_collectedEnoughAlready = false;
		}
	}

	// INPUT LISTENER METHODS
	// Receives the Vector2 representing the players arrow key input, rotated by the view angle
	public void HorizontalInput(InputAction.CallbackContext ctx)
	{
		intendedMoveDir = Quaternion.Euler(0f, 0f, -ViewCamera.eulerAngles.y) * ctx.ReadValue<Vector2>();
	}

	// Receives the call to jump
	public void JumpInput(InputAction.CallbackContext ctx)
	{
		if (!ctx.started || !CharControl.isGrounded) return;
		DoJump = true;
	}

	// Receives the call to interact
	public void InteractInput(InputAction.CallbackContext ctx)
	{
		if (interactHand == null) return;
		if (!ctx.started) return;

		interactHand.SelectHighlightedObject();
	}

	// Receiver used for various UI bollocks
	public void EscInput(InputAction.CallbackContext ctx)
	{
		if (!ctx.started) return;
		UiEscape.Invoke();
	}
}
