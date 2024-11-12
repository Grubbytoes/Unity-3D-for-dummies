using UnityEngine;

public class Collectable : MonoBehaviour
{        
	public static string itemName = "collectable";

    public virtual void OnCollectedBy(BasePlayerCharacter playerChar)
    {
        // Add reference to inventory
        playerChar.Inventory.Add(itemName);

        // Deactivate self
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checking the collided object is a player
        var playerChar = other.GetComponent<BasePlayerCharacter>();
        if (playerChar == null) return;
        
        playerChar.OnCollect(this);
        OnCollectedBy(playerChar);
    }
}
