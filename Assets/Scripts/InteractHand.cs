using UnityEngine;

class InteractHand : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        InteractableObject io = other.GetComponent<InteractableObject>();
        if (io == null) return;

        io.OnHighlighted();
    }
}