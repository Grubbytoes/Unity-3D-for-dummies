using UnityEngine;

class InteractHand : MonoBehaviour
{
	private InteractableObject highlighted;

	public bool ObjectHighlighted()
	{
		return highlighted != null;
	}

	public void Select()
	{
		if (!ObjectHighlighted()) return;
		highlighted.OnSelected();
	}

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("beep");

        InteractableObject io = other.GetComponent<InteractableObject>();
        if (io == null) return;

        io.OnHighlighted();
		highlighted = io;
    }

	private void OnTriggerExit(Collider other)
	{
		if (other == highlighted) highlighted = null;
	}
}