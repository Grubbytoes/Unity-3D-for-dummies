using System;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    public static event Action<string> DisplayTooltip;

    [SerializeField] protected string tooltip = "";
    [SerializeField] protected SpriteRenderer billboard;

    private bool _isHighlighted;
    public bool IsHighlighted
    {
        get { return _isHighlighted; }
        set
        {
            _isHighlighted = value;
            if (billboard != null) billboard.enabled = value; // Java trauma ass null checking

            if (value) DisplayTooltip?.Invoke(tooltip);
            else DisplayTooltip?.Invoke("");
        }
    }
}