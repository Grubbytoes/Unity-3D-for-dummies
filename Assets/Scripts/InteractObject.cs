using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InteractObject : MonoBehaviour
{
	public static event Action<string, bool> PopupText;

    private bool _isHighlighted;
    public bool IsHighlighted
    {
        get { return _isHighlighted; }
        set 
        { 
            _isHighlighted = value;
            billboard.enabled = value;
        }
    }
    

    [SerializeField] protected SpriteRenderer billboard;
    [SerializeField] protected string LongMessagePath;
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