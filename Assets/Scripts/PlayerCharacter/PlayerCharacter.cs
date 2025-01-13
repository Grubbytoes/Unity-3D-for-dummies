
using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerCharacter : BaseCharacter
{
	public static event Action CollectedEnoughItems;
	public static event Action UiEscape;

	public UnityEvent<string> ItemPickedUp;

	public float JumpPower = 5;
	public float MoveSpeed = 5;
	public readonly ItemStore Inventory = new();
	public Transform ViewCamera;
	public Noisemaker sfxNoisemaker;
	
	[SerializeField] protected InteractHand interactHand;
	[SerializeField] protected bool IsDisabled = false;
	protected Vector2 HorizontalInputDir;
	protected Vector2 HorizontalMoveDir;
	protected bool DoJump;

	private bool _collectedEnoughAlready = false;

	void Update()
	{
		finalMovement = Vector3.zero;

		ApplyHorizontalMovement();
		ApplyRotation();
		ApplyJump();
		ApplyGravity();

		CharControl.Move(finalMovement);

		SfxCycle();
	}

	// Apply a component along the XZ plane based on the horizontal input
	// Returns early, before making any changes to _finalMovement, if IsDisabled;
	private void ApplyHorizontalMovement()
	{
		if (IsDisabled) return;

		var deltaSpeed = Time.deltaTime * MoveSpeed;
		HorizontalMoveDir = Quaternion.Euler(0f, 0f, -ViewCamera.eulerAngles.y) * HorizontalInputDir;

		finalMovement.x += HorizontalMoveDir.x * deltaSpeed;
		finalMovement.z += HorizontalMoveDir.y * deltaSpeed;
	}

	// Rotates the character such that they are facing towards the direction of travel
	public void ApplyRotation()
	{
		if (HorizontalInputDir.magnitude < 0.1f) return;

		Quaternion toRotate = Quaternion.LookRotation(new Vector3(HorizontalMoveDir.x, 0f, HorizontalMoveDir.y));
		transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, 400f * Time.deltaTime);
	}

	// Applies upward velocity on the y axis, if a jump is queued (DoJump)
	// Returns early, before making any changes to _finalMovement, if IsDisabled;
	private void ApplyJump()
	{
		if (IsDisabled) return;

		if (!DoJump) return;
		DoJump = false;

		verticalMovement = JumpPower;
		finalMovement.y += JumpPower * Time.deltaTime;
	}

	private float _footstepTime = 0.5f;
	private void SfxCycle()
	{
		if (HorizontalInputDir.magnitude == 0)
		{
			_footstepTime = 0;
			return;
		}
		else  
		{
			_footstepTime -= Time.deltaTime;
		}

		if (_footstepTime <= 0  && CharControl.isGrounded)
		{
			_footstepTime = 0.5f;
			sfxNoisemaker.MakeNoise(0);
		}
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

	public void DisableControl()
	{
		Debug.Log("My control has been disabled");
		IsDisabled = true;
	}

	public void EnableControl()
	{
		IsDisabled = false;
	}

	// INPUT LISTENER METHODS
	// Receives the Vector2 representing the players arrow key input, rotated by the view angle
	public void HorizontalInput(InputAction.CallbackContext ctx)
	{
		HorizontalInputDir = ctx.ReadValue<Vector2>();
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
