using System;
using UnityEngine;
using UnityEngine.Events;

public class InteractObject : MonoBehaviour
{
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
    }
}