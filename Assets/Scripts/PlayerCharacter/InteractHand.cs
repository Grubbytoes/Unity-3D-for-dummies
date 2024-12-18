using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractHand : MonoBehaviour
{
    public static event Action Select;

    private HighlightObject _highlighted;
    public HighlightObject Highlighted
    {
        get
        {
            return _highlighted;
        }
        private set
        {
            if (_highlighted != null) _highlighted.IsHighlighted = false;
            _highlighted = value;
            if (value != null) value.IsHighlighted = true;
        }
    }

    public void SelectHighlightedObject()
    {
        Select.Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        var newObject = other.GetComponent<HighlightObject>();
        if (newObject == null) return;
        Highlighted = newObject;
    }

    void OnTriggerExit(Collider other)
    {
        var newObject = other.GetComponent<HighlightObject>();
        if (newObject == null) return;
        if (newObject != Highlighted) return;

        ClearHighlighted();
    }

    private void ClearHighlighted()
    {
        Highlighted = null;
    }
}