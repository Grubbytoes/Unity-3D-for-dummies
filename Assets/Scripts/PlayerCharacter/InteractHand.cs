using UnityEngine;

class InteractHand : MonoBehaviour
{
	protected InteractableObject HighlightedObject;

	public bool ObjectHighlighted()
	{
		return HighlightedObject != null;
	}

	public void TryInteract()
	{
		if (!ObjectHighlighted()) 
		{
			Debug.Log("Oops, no hightlighted object!");
			return;
		}

		Debug.Log("Interacting!");
		HighlightedObject.OnSelected();
	}

    private void OnTriggerEnter(Collider other) 
    {
		Debug.Log("Entered trigger");

        InteractableObject io = other.GetComponent<InteractableObject>();
        if (io == null) return;

        io.OnHighlighted();
		HighlightedObject = io;
    }

	private void OnTriggerExit(Collider other)
	{
		if (other == HighlightedObject) HighlightedObject = null;
	}
}