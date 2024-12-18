using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InteractObject : HighlightObject
{
	public static event Action<string, bool> PopupText;
    [SerializeField] protected string LongMessagePath = "Not set";
    [SerializeField] protected string ShortMessage;

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

    private void OnSelect()
    {
        if (!IsHighlighted) return;

        Debug.Log("I have been selected woop!");

        if (LongMessagePath != "")
        {
            Debug.Log("Writing a long message");
            PopupText.Invoke(LongMessagePath, true);
        }
        else if (ShortMessage != "")
        {
            Debug.Log("Writing a short message");
            PopupText.Invoke(ShortMessage, false);
        }
    }
}