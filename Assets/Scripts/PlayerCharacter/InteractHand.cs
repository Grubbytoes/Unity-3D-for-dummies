using UnityEngine;

class InteractHand : MonoBehaviour
{
	private InteractableObject highlighted;

	public bool ObjectHighlighted()
	{
		return highlighted != null;
	}

	public void TryInteract()
	{
		if (!ObjectHighlighted()) 
		{
			Debug.Log("Oops, no hightlighted object!");
			return;
		}

		Debug.Log("Interacting!");
		highlighted.OnSelected();
	}

    private void OnTriggerEnter(Collider other) 
    {
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