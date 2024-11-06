using System.ComponentModel.Design;
using UnityEngine;

public class InteractableObject : MonoBehaviour 
{
    public virtual void OnHighlighted()
    {
        Debug.Log("I HAVE BEEN HIGHLIGHTED");
        // TODO
    }
}