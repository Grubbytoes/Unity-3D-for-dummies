using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InteractObject : HighlightObject
{
	public static event Action<string> PopupText;

    [TextAreaAttribute]
    [SerializeField] 
    protected string ShortMessage;

    void Awake()
    {
        IsHighlighted = false;
        InteractHand.Select += OnSelect;
    }

    void Update()
    {
        if (IsHighlighted)
        {
            billboard.transform.LookAt(Camera.allCameras[0].transform.position, -Vector3.up);
        }
    }

    protected virtual void OnSelect()
    {
        if (!IsHighlighted) return;

        else if (ShortMessage != "")
        {
            PopupText.Invoke(ShortMessage);
        }
    }
}