using UnityEngine;

class InteractHand : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("beep");

        InteractableObject io = other.GetComponent<InteractableObject>();
        if (io == null) return;

        io.OnHighlighted();
    }
}