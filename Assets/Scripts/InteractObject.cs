using System;
using UnityEngine;
using UnityEngine.Events;

public class InteractObject : MonoBehaviour
{
    public bool IsHighlighted { get; set; }

    void Awake()
    {
        InteractHand.Select += OnSelect;
    }

    private void OnSelect()
    {
        if (!IsHighlighted) return;
        Debug.Log("I have been selected!!");
    }
}